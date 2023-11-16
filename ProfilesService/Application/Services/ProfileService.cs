using System.Net;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using Profile = ProfilesService.Domain.Models.Common.Profile;

namespace ProfilesService.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public ProfileService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public Task<List<CandidateProfile>> GetAllProfiles(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<CandidateProfile>().ToListAsync(cancellationToken);
    }

    public async Task<T> GetProfile<T>(Guid id, CancellationToken cancellationToken = default) 
        where T : Profile
    {
        var profile = await _repository.GetByIdAsync<T>(id);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profile;
    }

    public async Task CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default)
    {
        if (profile.Role == ProfileRole.Candidate)
        {
            var profileEntity = new CandidateProfile().MapCreateToCandidateProfile(profile);
            var result = await _repository.CreateAsync(profileEntity);
        }
        else
        {
            var profileEntity = new RecruiterProfile().MapCreateToRecruiterProfile(profile);
            var result = await _repository.CreateAsync(profileEntity);
        }
        
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<CandidateProfile> UpdateCandidateProfile(CandidateProfileUpdateDto profile, CancellationToken cancellationToken = default)
    {
        var profileEntity = _mapper.Map<CandidateProfile>(profile);
        var isExists = await _repository.AnyAsync<CandidateProfile>(x => x.Id == profileEntity.Id);
        if (!isExists)
        {
            throw new ExceptionWithStatusCode("Vacancy that you trying to update, not exist", HttpStatusCode.BadRequest);
        }
        var result = _repository.Update(profileEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<RecruiterProfile> UpdateRecruiterProfile(RecruiterProfileUpdateDto profile, CancellationToken cancellationToken = default)
    {
        var recruiterProfile = await _repository.GetByIdAsync<RecruiterProfile>(profile.Id);
        if (recruiterProfile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        recruiterProfile.MapUpdate(profile);
        var result = _repository.Update(recruiterProfile);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteProfile(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancy = await _repository.GetByIdAsync<CandidateProfile>(id);
        if (vacancy == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }
        _repository.Delete(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteProfileByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var profile = _repository.GetAll<CandidateProfile>().FirstOrDefault(p => p.UserId == userId);
        _repository.Delete(profile);
        return _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task ActivateDisactivateProfile(Guid id, CancellationToken cancellationToken = default)
    {
        var profile = await _repository.GetByIdAsync<CandidateProfile>(id);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        profile.IsActive = !profile.IsActive;
        _repository.Update(profile);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
