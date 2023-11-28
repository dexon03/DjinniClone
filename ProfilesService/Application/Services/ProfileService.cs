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
using Mapper = ProfilesService.Application.Mappers.Mapper;

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
        // var profileEntity = 
        //     from profile in  _repository.GetAll<T>().Where(p => p.UserId == userId) 
        //     join profileSkill in _repository.GetAll<ProfileSkills>() 
        //         on profile.Id equals profileSkill.ProfileId into profileSkills 
        //     from profileSkill in profileSkills.DefaultIfEmpty() 
        //     join skill in _repository.GetAll<Skill>() 
        //         on profileSkill.SkillId equals skill.Id into skills
        //     join profileLocation in _repository.GetAll<LocationProfile>() 
        //         on profile.Id equals profileLocation.ProfileId into profileLocations
        //     from profileLocation in profileLocations.DefaultIfEmpty()
        //     join location in _repository.GetAll<Location>() 
        //         on profileLocation.LocationId equals location.Id into locations
        //     select new
        //     {
        //         profile.Id,
        //         profile.Name,
        //         profile.Surname,
        //         profile.Email,
        //         profile.PhoneNumber,
        //         profile.DateBirth,
        //         profile.Description,
        //         profile.ImageUrl,
        //         profile.GitHubUrl,
        //         profile.LinkedInUrl,
        //         profile.PositionTitle,
        //         profile.IsActive,
        //         profile.,
        //         
            // }
            
        var profileEntity = await _repository.FirstOrDefaultAsync<T>( p=> p.UserId == userId);
        
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profileEntity.ToDto();
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

        _mapper.Map(profileDto, profileEntity);

        _repository.Update(profileEntity);
        await _repository.SaveChangesAsync(cancellationToken);

        return profileEntity;
    }

    public async Task DeleteProfile<T>(Guid id, CancellationToken cancellationToken = default) where T : Profile<T>
    {
        var profile = await _repository.GetByIdAsync<T>(id);
        if (profile == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        _repository.Delete(profile);
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
}
