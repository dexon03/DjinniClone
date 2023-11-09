using System.Net;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using Profile = ProfilesService.Domain.Models.Common.Profile;
using ValidationException = Core.Exceptions.ValidationException;

namespace ProfilesService.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProfileCreateDto> _createValidator;
    private readonly IValidator<CandidateProfileUpdateDto> _updateValidator;

    public ProfileService(IRepository repository, IMapper mapper, IValidator<ProfileCreateDto> createValidator, IValidator<CandidateProfileUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }


    public Task<List<CandidateProfile>> GetAllProfiles(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<CandidateProfile>().ToListAsync(cancellationToken);
    }

    public async Task<CandidateProfile> GetProfileById(Guid id, CancellationToken cancellationToken = default)
    {
        var profile = await _repository.GetByIdAsync<CandidateProfile>(id);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profile;
    }

    public async Task CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default)
    {
        var validationResult =  await _createValidator.ValidateAsync(profile,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var profileEntity = new Profile();
        if (profile.Role == ProfileRole.Candidate)
        {
            profileEntity = profileEntity.MapCreateToCandidateProfile();
        }
        else
        {
            profileEntity = profileEntity.MapCreateToRecruiterProfile();
        }
        
        var result = await _repository.CreateAsync(profileEntity);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<CandidateProfile> UpdateCandidateProfile(CandidateProfileUpdateDto profile, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(profile,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
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
