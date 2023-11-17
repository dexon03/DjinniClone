using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Contracts;

public interface IProfileService
{
    Task<List<CandidateProfile>> GetAllProfiles(CancellationToken cancellationToken = default);
    Task<IProfileDto<T>> GetProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>;
    Task CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default);
    Task<CandidateProfile> UpdateCandidateProfile(CandidateProfileUpdateDto profile, CancellationToken cancellationToken = default);
    Task<RecruiterProfile> UpdateRecruiterProfile(RecruiterProfileUpdateDto profile, CancellationToken cancellationToken = default);
    Task DeleteProfile(Guid id, CancellationToken cancellationToken = default);
    Task DeleteProfileByUserId(Guid userId, CancellationToken cancellationToken = default);
    
    Task ActivateDisactivateProfile(Guid id, CancellationToken cancellationToken = default);
}