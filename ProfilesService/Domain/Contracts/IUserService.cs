using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Domain.Contracts;

public interface IUserService
{
    Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<User> GetUserById(Guid id, CancellationToken cancellationToken = default);
    Task<User> CreateUser(UserCreateDto user, CancellationToken cancellationToken = default);
    Task<User> UpdateUser(UserUpdateDto user, CancellationToken cancellationToken = default);
    Task DeleteUser(Guid id, CancellationToken cancellationToken = default);
}