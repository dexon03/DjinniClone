using System.Net;
using AutoMapper;
using Core.Database;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

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

    public async Task<IProfileDto<T>> GetProfile<T>(Guid userId, CancellationToken cancellationToken = default) 
        where T : Profile<T>
    {
        var profile = await _repository.FirstOrDefaultAsync<T>(p => p.UserId == userId);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profile.ToDto();
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
    
    public async Task<T> UpdateProfile<T>(IProfileUpdateDto<T> profileDto, CancellationToken cancellationToken = default) where T : Profile<T>
    {
        var profileEntity = await _repository.GetByIdAsync<T>(profileDto.Id);

        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile that you trying to update, not exist", HttpStatusCode.BadRequest);
        }

        MapProperties(profileDto, profileEntity);

        _repository.Update(profileEntity);
        await _repository.SaveChangesAsync(cancellationToken);

        return profileEntity;
    }
    
    public async Task<TProfile> UpdateProfile<TProfile, TUpdateDto>(IProfileUpdateDto<TProfile> profile, CancellationToken cancellationToken = default)
        where TProfile : Profile<TProfile>
        where TUpdateDto : IProfileUpdateDto<TProfile>
    {
        var profileEntity = await _repository.GetByIdAsync<TProfile>(profile.Id);
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile that you are trying to update does not exist", HttpStatusCode.BadRequest);
        }

        // Use reflection to copy properties from the update DTO to the profile entity
        var profileProperties = typeof(TProfile).GetProperties();
        var updateDtoProperties = typeof(TUpdateDto).GetProperties();

        foreach (var propertyInfo in updateDtoProperties)
        {
            var propertyName = propertyInfo.Name;
            var profileProperty = profileProperties.FirstOrDefault(p => p.Name == propertyName);

            if (profileProperty != null && profileProperty.CanWrite)
            {
                var value = propertyInfo.GetValue(profile);
                profileProperty.SetValue(profileEntity, value);
            }
        }

        var result = _repository.Update(profileEntity);
        await _repository.SaveChangesAsync(cancellationToken);
        return result;
    }


    public async Task DeleteProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>
    {
        var profile = await _repository.GetByIdAsync<T>(id);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Vacancy not found", HttpStatusCode.BadRequest);
        }
        _repository.Delete<T>(profile);
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteProfileByUserId<T>(Guid userId, CancellationToken cancellationToken = default) where T : Profile<T>
    {
        var profile = _repository.GetAll<T>().FirstOrDefault(p => p.UserId == userId);
        _repository.Delete<T>(profile);
        return _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task ActivateDisactivateProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>
    {
        var profile = await _repository.GetByIdAsync<T>(id);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        profile.IsActive = !profile.IsActive;
        _repository.Update(profile);
        await _repository.SaveChangesAsync(cancellationToken);
    }
    
    private void MapProperties<TUpdateDto, TEntity>(TUpdateDto updateDto, TEntity profileEntity) 
        where TUpdateDto : IProfileUpdateDto<TEntity>
    {
        var profileProperties = typeof(TEntity).GetProperties();
        var updateDtoProperties = typeof(TUpdateDto).GetProperties();
        foreach (var propertyInfo in updateDtoProperties)
        {
            var propertyName = propertyInfo.Name;
            var profileProperty = profileProperties.FirstOrDefault(p => p.Name == propertyName);

            if (profileProperty != null && profileProperty.CanWrite)
            {
                var value = propertyInfo.GetValue(updateDto);
                profileProperty.SetValue(profileEntity, value);
            }
        }
    }
}
