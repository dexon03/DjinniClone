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
        return await _repository.FirstOrDefaultAsync<User>(u => u.Email == email);
    } 
    
    public async Task<User?> FindByIdAsync(Guid id)
    {
        return await _repository.FirstOrDefaultAsync<User>(u => u.Id == id);
    }
    
    public async Task<bool> CheckPasswordAsync(Guid id, string password)
    {
        var user = await _repository.FirstOrDefaultAsync<User>(u => u.Id == id);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var hashedPassword = PasswordUtility.GetHashedPassword(password, user.PasswordSalt);
        return hashedPassword == user.PasswordHash;
    }
}