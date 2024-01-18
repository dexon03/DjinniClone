using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Contracts;

public interface IProfileService
{
    Task<List<GetCandidateProfileDto>> GetAllCandidatesProfiles(CandidateFilterParameters filter,CancellationToken cancellationToken = default);
    Task<GetRecruiterProfileDto> GetRecruiterProfile(Guid recruiterId);
    Task<GetCandidateProfileDto> GetCandidateProfile(Guid profileId, CancellationToken cancellationToken = default);
    Task<GetCandidateProfileDto> GetCandidateProfileByUserId(Guid userId, CancellationToken cancellationToken = default);
    Task<GetRecruiterProfileDto> GetRecruiterProfileByUserId(Guid userId, CancellationToken cancellationToken = default);
    Task CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default);
    Task<CandidateProfile> UpdateCandidateProfile(CandidateProfileUpdateDto profileDto, CancellationToken cancellationToken = default);
    Task<RecruiterProfile> UpdateRecruiterProfile(RecruiterProfileUpdateDto profileDto, CancellationToken cancellationToken = default);
    Task UploadResume(ResumeUploadDto resumeDto, CancellationToken cancellationToken = default);
    Task<byte[]?> DownloadResume(Guid candidateId, CancellationToken cancellationToken = default);
    Task DeleteProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>;
    Task DeleteProfileByUserId<T>(Guid userId, CancellationToken cancellationToken = default) where T : Profile<T>;
    Task ActivateDisactivateProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>;
}