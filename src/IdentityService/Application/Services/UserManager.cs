using System.Net;
using Core.Database;
using Core.Exceptions;
using Core.MessageContract;
using IdentityService.Application.Utilities;
using IdentityService.Domain.Dto;
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

    public  Task<List<User>> GetUsers()
    {
        return (from u in _repository.GetAll<User>()
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
        var user = await 
            _repository.GetAll<User>()
                .Where(u => u.Id == id)
                .AsSplitQuery()
            .Include(u => u.Role)
            .Select(u => new User
            {
                Id = u.Id,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt,
                Role = u.Role,
                RoleId = u.RoleId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PhoneNumber = u.PhoneNumber,
                RefreshToken = u.RefreshToken,
            }).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new Exception("User not found by id");
        }

        return user;
    }

    public async Task<User> CreateUser(RegisterRequest request, CancellationToken cancellationToken)
    {
        var role = await _repository.GetAsync<Role>(r => r.Name == request.Role.ToString() && r.IsActive);
        if (role == null)
        {
            throw new ExceptionWithStatusCode("Role does not exist or no active", HttpStatusCode.BadRequest);
        }

        var passwordSalt = PasswordUtility.CreatePasswordSalt();
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordSalt = passwordSalt,
            PasswordHash = PasswordUtility.GetHashedPassword(request.Password, passwordSalt),
            RoleId = role.Id,
            Role = role,
        };
        return user;
    }
    
    public async Task<User> UpdateUser(UpdateUserRequest request)
    {
        var user = await FindByIdAsync(request.Id);
        if(user is null)
        {
            throw new ExceptionWithStatusCode("User not found", HttpStatusCode.BadRequest);
        }
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        _repository.Update(user);
        await _repository.SaveChangesAsync();
        await _publishEndpoint.Publish(new UserUpdatedEvent
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = request.Role
        });
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