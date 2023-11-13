using Core.Database;
using Core.MessageContract;
using IdentityService.Application.Utilities;
using IdentityService.Domain.Models;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Application.Services;

public class UserManager
{
    private readonly IRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public UserManager(IRepository repository, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<List<User>> GetUsers()
    {
        return await (from u in _repository.GetAll<User>()
            join r in _repository.GetAll<Role>() on u.RoleId equals r.Id
            select new User
            {
                Id = u.Id,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt,
                Role = r,
                RoleId = r.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                RefreshToken = u.RefreshToken,
            }).ToListAsync(); 
    }
    
    public async Task<User?> FindByEmailAsync(string email)
    {
        var user =  await (from u in _repository.GetAll<User>()
            where u.Email == email
                join r in _repository.GetAll<Role>() on u.RoleId equals r.Id
            select new User
            {
                Id = u.Id,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt,
                Role = r,
                RoleId = r.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                RefreshToken = u.RefreshToken,
            }).AsNoTracking().FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("Wrong email");
        }

        return user;
    } 
    
    public async Task<User?> FindByIdAsync(Guid id)
    {
        var user = await (from u in _repository.GetAll<User>()
            where u.Id == id
            join r in _repository.GetAll<Role>() on u.RoleId equals r.Id
            select new User
            {
                Id = u.Id,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt,
                Role = r,
                RoleId = r.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                RefreshToken = u.RefreshToken,
            }).AsNoTracking().FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("User not found by email");
        }

        return user;
    }
    
    public Task<bool> CheckPasswordAsync(User user, string password)
    {
        var hashedPassword = PasswordUtility.GetHashedPassword(password, user.PasswordSalt);
        return Task.FromResult(hashedPassword == user.PasswordHash);
    }
    
    public async Task DeleteUser(Guid id)
    {
        var user = await FindByIdAsync(id);
        _repository.Delete(user);
        await _repository.SaveChangesAsync();
        await _publishEndpoint.Publish(new UserDeletedEvent
        {
            UserId = id
        });
    }
}