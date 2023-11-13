using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Domain.Contracts;

public interface IProfileService
{
    Task<List<CandidateProfile>> GetAllProfiles(CancellationToken cancellationToken = default);
    Task<CandidateProfile> GetProfileById(Guid id, CancellationToken cancellationToken = default);
    Task CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default);
    Task<CandidateProfile> UpdateCandidateProfile(CandidateProfileUpdateDto profile, CancellationToken cancellationToken = default);
    Task<RecruiterProfile> UpdateRecruiterProfile(RecruiterProfileUpdateDto profile, CancellationToken cancellationToken = default);
    Task DeleteProfile(Guid id, CancellationToken cancellationToken = default);
    Task DeleteProfileByUserId(Guid userId, CancellationToken cancellationToken = default);
    
    Task ActivateDisactivateProfile(Guid id, CancellationToken cancellationToken = default);
}