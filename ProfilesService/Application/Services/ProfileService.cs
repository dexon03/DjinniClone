using System.Net;
using AutoMapper;
using Core.Database;
using Core.Enums;
using Core.Exceptions;
using Core.MessageContract;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IPdfService _pdfService;

    public ProfileService(
        IRepository repository, 
        IMapper mapper, 
        IPublishEndpoint publishEndpoint,
        IPdfService pdfService)
    {
        _repository = repository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _pdfService = pdfService;
    }
    
    public async Task<List<GetCandidateProfileDto>> GetAllCandidatesProfiles(CandidateFilterParameters filter,CancellationToken cancellationToken = default)
    {
        var profileQuery = _repository.GetAll<CandidateProfile>();
        
        if(filter.searchTerm is not null)
        {
            profileQuery = profileQuery.Where(p => p.Name.ToLower().Contains(filter.searchTerm.ToLower())
                                                   || p.Surname.ToLower().Contains(filter.searchTerm.ToLower())
                                                   || p.PositionTitle.ToLower().Contains(filter.searchTerm.ToLower())
                                                   || p.Description.ToLower().Contains(filter.searchTerm.ToLower()));
        }
        
        if (filter.skill is not null)
        {
            profileQuery = profileQuery?
                .Include(p => p.ProfileSkills)!.ThenInclude(ps => ps.Skill)
                .Where(p => p.ProfileSkills.Any(ps => filter.skill == ps.Skill.Id));
                
        }
        
        if (filter.location is not null)
        {
            profileQuery = profileQuery?
                .Include(p => p.LocationProfiles)!.ThenInclude(lp => lp.Location)
                .Where(p => p.LocationProfiles.Any(lp => filter.location == lp.Location.Id));
        }
        
        if (filter.experience is not null)
        {
            profileQuery = profileQuery.Where(p => p.WorkExperience == filter.experience);
        }
        
        if (filter.attendanceMode is not null)
        {
            profileQuery = profileQuery.Where(p => p.Attendance == filter.attendanceMode);
        }
        
        var profileEntities =
            (await (from profile in profileQuery
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
                    profile.Attendance,
                    SkillId = skill != null ? skill.Id : Guid.Empty,
                    SkillName = skill != null ? skill.Name : String.Empty,
                    LocationId = location != null ? location.Id : Guid.Empty,
                    LocationCity = location != null ? location.City : String.Empty,
                    LocationCountry = location != null ? location.Country : String.Empty
                })
                .Skip((filter.page-1) * filter.pageSize)
                .Take(filter.pageSize)
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
                p.DesiredSalary,
                p.Attendance
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
                Attendance = gp.Key.Attendance,
                Skills = gp.All(_ => _.SkillId != Guid.Empty)
                    ? gp.Select(p => new
                    {
                        Id = p.SkillId,
                        Name = p.SkillName
                    }).GroupBy(sd => new
                    {
                        sd.Id,
                        sd.Name
                    }).Select(gs => new SkillDto
                    {
                        Id = gs.Key.Id,
                        Name = gs.Key.Name
                    })
                    : new List<SkillDto>(),
                Locations = gp.All(_ => _.LocationId != Guid.Empty)
                    ? gp.Select(p => new
                    {
                        Id = p.LocationId,
                        City = p.LocationCity,
                        Country = p.LocationCountry
                    }).GroupBy(l => new
                    {
                        l.Id,
                        l.City,
                        l.Country
                    }).Select(gl => new LocationGetDto
                    {
                        Id = gl.Key.Id,
                        City = gl.Key.City,
                        Country = gl.Key.Country
                    })
                    : new List<LocationGetDto>()
            }).ToList();
        
        return profileEntities;
    }

    public async Task<GetRecruiterProfileDto> GetRecruiterProfile(Guid recruiterId)
    {
        var profileEntity = await _repository.GetByIdAsync<RecruiterProfile>(recruiterId);
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }

        var profile = new GetRecruiterProfileDto
        {
            Id = profileEntity.Id,
            Name = profileEntity.Name,
            Surname = profileEntity.Surname,
            Email = profileEntity.Email ?? String.Empty,
            PhoneNumber = profileEntity.PhoneNumber ?? String.Empty,
            DateBirth = profileEntity.DateBirth,
            Description = profileEntity.Description ?? String.Empty,
            ImageUrl = profileEntity.ImageUrl ?? String.Empty,
            LinkedInUrl = profileEntity.LinkedInUrl ?? String.Empty,
            PositionTitle = profileEntity.PositionTitle ?? String.Empty,
            IsActive = profileEntity.IsActive,
            UserId = profileEntity.UserId
        };
        
        return profile;
    }

    public async Task<GetCandidateProfileDto> GetCandidateProfile(Guid profileId, CancellationToken cancellationToken = default)
    {
        var profileEntity =
            (await (from profile in _repository.GetAll<CandidateProfile>().Where(p => p.Id == profileId)
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
                    profile.Attendance,
                    profile.UserId,
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
                p.DesiredSalary,
                p.Attendance,
                p.UserId
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
                Attendance = gp.Key.Attendance,
                UserId = gp.Key.UserId,
                Skills = gp.All(_ => _.SkillId != Guid.Empty)
                    ? gp.Select(p => new
                    {
                        Id = p.SkillId,
                        Name = p.SkillName
                    }).GroupBy(sd => new
                    {
                        sd.Id,
                        sd.Name
                    }).Select(gs => new SkillDto
                    {
                        Id = gs.Key.Id,
                        Name = gs.Key.Name
                    })
                    : new List<SkillDto>(),
                Locations = gp.All(_ => _.LocationId != Guid.Empty)
                    ? gp.Select(p => new
                    {
                        Id = p.LocationId,
                        City = p.LocationCity,
                        Country = p.LocationCountry
                    }).GroupBy(l => new
                    {
                        l.Id,
                        l.City,
                        l.Country
                    }).Select(gl => new LocationGetDto
                    {
                        Id = gl.Key.Id,
                        City = gl.Key.City,
                        Country = gl.Key.Country
                    })
                    : new List<LocationGetDto>()
            }).FirstOrDefault();
        
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profileEntity;
    }

    public async Task<GetCandidateProfileDto> GetCandidateProfileByUserId(Guid userId, CancellationToken cancellationToken = default)
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
                    profile.Attendance,
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
                p.DesiredSalary,
                p.Attendance
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
                Attendance = gp.Key.Attendance,
                UserId = userId,
                Skills = gp.All(_ => _.SkillId != Guid.Empty)
                    ? gp.Select(p => new
                    {
                        Id = p.SkillId,
                        Name = p.SkillName
                    }).GroupBy(sd => new
                    {
                        sd.Id,
                        sd.Name
                    }).Select(gs => new SkillDto
                    {
                        Id = gs.Key.Id,
                        Name = gs.Key.Name
                    })
                    : new List<SkillDto>(),
                Locations = gp.All(_ => _.LocationId != Guid.Empty)
                    ? gp.Select(p => new
                    {
                        Id = p.LocationId,
                        City = p.LocationCity,
                        Country = p.LocationCountry
                    }).GroupBy(l => new
                    {
                        l.Id,
                        l.City,
                        l.Country
                    }).Select(gl => new LocationGetDto
                    {
                        Id = gl.Key.Id,
                        City = gl.Key.City,
                        Country = gl.Key.Country
                    })
                    : new List<LocationGetDto>()
            }).FirstOrDefault();
        
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profileEntity;
    }
    
    public async Task<GetRecruiterProfileDto> GetRecruiterProfileByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        var profileEntity = 
           await _repository.GetAll<RecruiterProfile>()
                .Where(rp => rp.UserId == userId)
                .AsSplitQuery()
            .Include(rp => rp.Company)
            .Select(t => new GetRecruiterProfileDto
            {
                Id = t.Id,
                Name = t.Name,
                Surname = t.Surname,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber ,
                DateBirth = t.DateBirth,
                Description = t.Description,
                ImageUrl = t.ImageUrl,
                LinkedInUrl = t.LinkedInUrl,
                PositionTitle = t.PositionTitle,
                IsActive = t.IsActive,
                Company = t.Company,
                UserId = t.UserId,
            }).FirstOrDefaultAsync(cancellationToken);
        
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile not found", HttpStatusCode.BadRequest);
        }
        
        return profileEntity;
    }

    public async Task CreateProfile(ProfileCreateDto profile, CancellationToken cancellationToken = default)
    {
        if (profile.Role == Role.Candidate)
        {
            var profileEntity = new CandidateProfile().MapCreateToCandidateProfile(profile);
            await _repository.CreateAsync(profileEntity);
        }
        else
        {
            var profileEntity = new RecruiterProfile().MapCreateToRecruiterProfile(profile);
            await _repository.CreateAsync(profileEntity);
        }
        
        await _repository.SaveChangesAsync(cancellationToken);
    }

    public async Task<CandidateProfile> UpdateCandidateProfile(CandidateProfileUpdateDto profileDto, CancellationToken cancellationToken = default)
    {
        var profile = await GetProfile(profileDto);

        if (profileDto.PdfResume is not null)
        {
            await UploadPdf(profile.Id, profileDto.PdfResume, cancellationToken);
        }
        
        _repository.Update(profile);
        await _repository.SaveChangesAsync(cancellationToken);
        
        return profile;
    }

    public async Task<RecruiterProfile> UpdateRecruiterProfile(RecruiterProfileUpdateDto profileDto, CancellationToken cancellationToken = default)
    {
        var profile = await GetProfile(profileDto);
        
        _repository.Update(profile);
        await _repository.SaveChangesAsync(cancellationToken);
        
        return profile;
    }

    public async Task UploadResume(ResumeUploadDto resumeDto, CancellationToken cancellationToken = default)
    {
        var isCandidateExists = await _repository.AnyAsync<CandidateProfile>(cp => cp.Id == resumeDto.CandidateId);
        if (!isCandidateExists)
        {
            throw new ExceptionWithStatusCode("Candidate profile not found", HttpStatusCode.BadRequest);
        }
        await UploadPdf(resumeDto.CandidateId, resumeDto.Resume, cancellationToken);
    }
    
    public async Task<byte[]?> DownloadResume(Guid candidateId, CancellationToken cancellationToken = default)
    {
        var candidateProfile = await _repository.GetByIdAsync<CandidateProfile>(candidateId);
        if (candidateProfile == null)
        {
            throw new ExceptionWithStatusCode("Candidate profile not found", HttpStatusCode.BadRequest);
        }

        if (_pdfService.CheckIfResumeFolderInitialised(candidateId))
        {
            var result = await _pdfService.DownloadPdf(candidateId, cancellationToken);
            return result;
        }
        return null;
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

    public async Task DeleteProfileByUserId<T>(Guid userId, CancellationToken cancellationToken = default) where T : Profile<T>
    {
        var profile = _repository.GetAll<T>().FirstOrDefault(p => p.UserId == userId);
        if (profile is not null)
        {
            _repository.Delete(profile);
            await _repository.SaveChangesAsync(cancellationToken);
            if (typeof(T) == typeof(CandidateProfile))
            {
                await _pdfService.DeletePdf(profile.Id);
            }
            if (typeof(T) == typeof(RecruiterProfile))
            {
                await _publishEndpoint.Publish(new RecruiterProfileDeletedEvent
                {
                    ProfileId = profile.Id
                }, cancellationToken);
            }
        }
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

    private async Task<T> GetProfile<T>(IProfileUpdateDto<T> profileDto) where T : Profile<T>
    {
        var profileEntity = await _repository.GetByIdAsync<T>(profileDto.Id);
        if (profileEntity == null)
        {
            throw new ExceptionWithStatusCode("Profile that you trying to update, not exist", HttpStatusCode.BadRequest);
        }
        _mapper.Map(profileDto, profileEntity);
        return profileEntity;
    }
    
    private async Task UploadPdf(Guid profileId, IFormFile formFile, CancellationToken cancellationToken = default)
    {
        if (Path.GetExtension(formFile.FileName) != ".pdf")
        {
            throw new ExceptionWithStatusCode("File must be pdf", HttpStatusCode.BadRequest);
        }

        if (!await _pdfService.CheckIfPdfExistsAndEqual(profileId, formFile, cancellationToken))
        {
            await _pdfService.UploadPdf(formFile, profileId, cancellationToken);
        }
    }
}
