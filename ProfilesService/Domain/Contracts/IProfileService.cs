using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Domain.Contracts;

public interface IProfileService
{
    Task<List<Profile>> GetAllProfiles(CancellationToken cancellationToken = default);
    Task<Profile> GetProfileById(Guid id, CancellationToken cancellationToken = default);
    Task<Profile> CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default);
    Task<Profile> UpdateProfile(ProfileUpdateDto profile, CancellationToken cancellationToken = default);
    Task DeleteProfile(Guid id, CancellationToken cancellationToken = default);
}