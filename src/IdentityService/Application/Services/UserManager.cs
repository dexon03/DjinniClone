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

public class UserManager(IRepository repository, IPublishEndpoint publishEndpoint)
{
    public Task<List<User>> GetUsers(int pageNumber, int pageSize)
    {
        return repository.GetAll<User>()
            .Include(x => x.Role)
            .OrderBy(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var user = await repository.GetAll<User>()
            .Include(x => x.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Equals(email));
        if (user is null)
        {
            throw new Exception("Wrong email");
        }

        return user;
    }

    public async Task<User?> FindByIdAsync(Guid id)
    {
        var user = await
            repository.GetAll<User>()
                .Where(u => u.Id == id)
                .AsSplitQuery()
                .Include(u => u.Role)
                .FirstOrDefaultAsync();
        if (user is null)
        {
            throw new ExceptionWithStatusCode($"User with id {id} not found", HttpStatusCode.BadRequest);
        }

        return user;
    }

    public async Task<User> CreateUser(RegisterRequest request)
    {
        var role = await repository.GetAsync<Role>(r => r.Name == request.Role.ToString() && r.IsActive);
        if (role is null)
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
            CreatedAt = DateTime.UtcNow,
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

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        repository.Update(user);
        await repository.SaveChangesAsync();
        await publishEndpoint.Publish(new UserUpdatedEvent
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


    public bool CheckPassword(User user, string password)
    {
        var hashedPassword = PasswordUtility.GetHashedPassword(password, user.PasswordSalt);
        return hashedPassword == user.PasswordHash;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await FindByIdAsync(id);
        repository.Delete(user);
        await repository.SaveChangesAsync();
        await publishEndpoint.Publish(new UserDeletedEvent
        {
            UserId = id
        });
    }
}