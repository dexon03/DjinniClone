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

    public async Task<GetCandidateProfileDto> GetCandidateProfile(Guid userId, CancellationToken cancellationToken = default)
    {
        var profileEntity =
            (await (from profile in _repository.GetAll<CandidateProfile>().Where(p => p.UserId == userId)
                join profileSkill in _repository.GetAll<ProfileSkills>()
                    on profile.Id equals profileSkill.ProfileId into profileSkills
                from profileSkill in profileSkills.DefaultIfEmpty()
                join skill in _repository.GetAll<Skill>()
                    on profileSkill.SkillId equals skill.Id into skills
                from skill in skills.DefaultIfEmpty()
                join profileLocation in _repository.GetAll<LocationProfile>()
                    on profile.Id equals profileLocation.ProfileId into profileLocations
                from profileLocation in profileLocations.DefaultIfEmpty()
                join location in _repository.GetAll<Location>()
                    on profileLocation.LocationId equals location.Id into locations
                from location in locations.DefaultIfEmpty()
                select new
                {
                    ProfileId = profile.Id,
                    ProfileName = profile.Name,
                    profile.Surname,
                    profile.Email,
                    profile.PhoneNumber,
                    profile.DateBirth,
                    profile.Description,
                    profile.ImageUrl,
                    profile.GitHubUrl,
                    profile.LinkedInUrl,
                    profile.PositionTitle,
                    profile.IsActive,
                    profile.WorkExperience,
                    profile.DesiredSalary,
                    SkillId = skill != null ? skill.Id : Guid.Empty,
                    SkillName = skill != null ? skill.Name : String.Empty,
                    LocationId = location != null ? location.Id : Guid.Empty,
                    LocationCity = location != null ? location.City : String.Empty,
                    LocationCountry = location != null ? location.Country : String.Empty
                })
            .ToListAsync(cancellationToken))
            .GroupBy(p => new
            {
                p.ProfileId,
                p.ProfileName,
                p.Surname,
                p.Email,
                p.PhoneNumber,
                p.DateBirth,
                p.Description,
                p.ImageUrl,
                p.GitHubUrl,
                p.LinkedInUrl,
                p.PositionTitle,
                p.IsActive,
                p.WorkExperience,
                p.DesiredSalary
            })
            .Select(gp => new GetCandidateProfileDto
            {
                Id = gp.Key.ProfileId,
                Name = gp.Key.ProfileName,
                Surname = gp.Key.Surname,
                Email = gp.Key.Email ?? String.Empty,
                PhoneNumber = gp.Key.PhoneNumber ?? String.Empty,
                DateBirth = gp.Key.DateBirth,
                Description = gp.Key.Description ?? String.Empty,
                ImageUrl = gp.Key.ImageUrl ?? String.Empty,
                GitHubUrl = gp.Key.GitHubUrl ?? String.Empty,
                LinkedInUrl = gp.Key.LinkedInUrl ?? String.Empty,
                PositionTitle = gp.Key.PositionTitle ?? String.Empty,
                IsActive = gp.Key.IsActive,
                WorkExperience = gp.Key.WorkExperience,
                DesiredSalary = gp.Key.DesiredSalary,
                Skills = gp.All(_ => _.SkillId != Guid.Empty) ? gp.Select(p => new SkillDto
                {
                    Id = p.SkillId,
                    Name = p.SkillName
                }) : new List<SkillDto>(),
                Locations = gp.All(_ => _.LocationId != Guid.Empty) ? gp.Select(p => new LocationGetDto
                {
                    Id = p.LocationId,
                    City = p.LocationCity,
                    Country = p.LocationCountry
                }) : new List<LocationGetDto>()
            }).FirstOrDefault();
        
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profileEntity;
    }
    
    public async Task<GetRecruiterProfileDto> GetRecruiterProfile(Guid userId, CancellationToken cancellationToken = default)
    {
        var profileEntity = await (
            from profile in _repository.GetAll<RecruiterProfile>().Where(rp => rp.UserId == userId)
            join company in _repository.GetAll<Company>() on profile.CompanyId equals company.Id into companies
            from company in companies.DefaultIfEmpty()
            select new GetRecruiterProfileDto()
            {
                Id = profile.Id,
                Name = profile.Name,
                Surname = profile.Surname,
                Email = profile.Email ?? String.Empty,
                PhoneNumber = profile.PhoneNumber ?? String.Empty,
                DateBirth = profile.DateBirth,
                Description = profile.Description ?? String.Empty,
                ImageUrl = profile.ImageUrl ?? String.Empty,
                LinkedInUrl = profile.LinkedInUrl ?? String.Empty,
                PositionTitle = profile.PositionTitle ?? String.Empty,
                IsActive = profile.IsActive,
                Company = company
            }).FirstOrDefaultAsync(cancellationToken);
        
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profileEntity;
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
