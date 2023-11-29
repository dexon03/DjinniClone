using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Contracts;

public interface IProfileService
{
    Task<List<CandidateProfile>> GetAllProfiles(CancellationToken cancellationToken = default);
    Task<GetCandidateProfileDto> GetCandidateProfile(Guid userId, CancellationToken cancellationToken = default);
    Task<GetRecruiterProfileDto> GetRecruiterProfile(Guid userId, CancellationToken cancellationToken = default);
    Task CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default);
    Task<T> UpdateProfile<T>(IProfileUpdateDto<T> profileDto, CancellationToken cancellationToken = default)
        where T : Profile<T>;
    // Task<CandidateProfile> UpdateCandidateProfile(CandidateProfileUpdateDto profile, CancellationToken cancellationToken = default);
    // Task<RecruiterProfile> UpdateRecruiterProfile(RecruiterProfileUpdateDto profile, CancellationToken cancellationToken = default);
    Task DeleteProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>;
    Task DeleteProfileByUserId<T>(Guid userId, CancellationToken cancellationToken = default) where T : Profile<T>;
    Task ActivateDisactivateProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>;
}