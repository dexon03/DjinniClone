using System.Net;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using Profile = ProfilesService.Domain.Models.Profile;
using ValidationException = FluentValidation.ValidationException;

namespace ProfilesService.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProfileCreateDto> _createValidator;
    private readonly IValidator<ProfileUpdateDto> _updateValidator;

    public ProfileService(IRepository repository, IMapper mapper, IValidator<ProfileCreateDto> createValidator, IValidator<ProfileUpdateDto> updateValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }


    public Task<List<Profile>> GetAllProfiles(CancellationToken cancellationToken = default)
    {
        return _repository.GetAll<Profile>().ToListAsync(cancellationToken);
    }

    public async Task<Profile> GetProfileById(Guid id, CancellationToken cancellationToken = default)
    {
        var profile = await _repository.GetByIdAsync<Profile>(id);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profile;
    }

    public async Task<Profile> CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default)
    {
        var validationResult =  await _createValidator.ValidateAsync(profile,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var profileEntity = _mapper.Map<Profile>(profile);
        var result = await _repository.CreateAsync(profileEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<Profile> UpdateProfile(ProfileUpdateDto profile, CancellationToken cancellationToken = default)
    {
        var validationResult = await _updateValidator.ValidateAsync(profile,cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var profileEntity = _mapper.Map<Profile>(profile);
        var isExists = await _repository.AnyAsync<Profile>(x => x.Id == profileEntity.Id);
        if (!isExists)
        {
            throw new ExceptionWithStatusCode("Vacancy that you trying to update, not exist", HttpStatusCode.BadRequest);
        }
        var result = _repository.Update(profileEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task DeleteProfile(Guid id, CancellationToken cancellationToken = default)
    {
        var vacancy = await _repository.GetByIdAsync<Profile>(id);
        if (vacancy == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }
        _repository.Delete(vacancy);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}
