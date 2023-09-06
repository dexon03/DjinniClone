using Core.Database;
using IdentityService.Application.Utilities;
using IdentityService.Domain.Models;

namespace IdentityService.Application.Services;

public class UserManager
{
    private readonly IRepository _repository;

    public UserManager(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<User?> FindByEmailAsync(string email)
    {
        var user =  await _repository.FirstOrDefaultAsync<User>(u => u.Email == email);
        if (user == null)
        {
            throw new Exception("Wrong email");
        }

        return user;
    } 
    
    public async Task<User?> FindByIdAsync(Guid id)
    {
        var user = await _repository.FirstOrDefaultAsync<User>(u => u.Id == id);
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
}