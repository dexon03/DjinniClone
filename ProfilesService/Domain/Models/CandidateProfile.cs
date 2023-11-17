using System.Text.Json.Serialization;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Enums;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class CandidateProfile : Profile<CandidateProfile>
{
    public Experience WorkExperience { get; set; } = Experience.NoExperience;
    public double DesiredSalary { get; set; } = 0;
    [JsonIgnore]public ICollection<ProfileSkills>? ProfileSkills { get; set; }
    [JsonIgnore]public ICollection<LocationProfile>? LocationProfiles { get; set; }
    public override IProfileDto<CandidateProfile> ToDto()
    {
        return new GetCandidateProfileDto
        {
            Id = Id,
            Name = Name,
            Surname = Surname,
            Email = Email,
            PhoneNumber = PhoneNumber,
            DateBirth = DateBirth,
            Description = Description,
            ImageUrl = ImageUrl,
            GitHubUrl = GitHubUrl,
            LinkedInUrl = LinkedInUrl,
            PositionTitle = PositionTitle,
            IsActive = IsActive,
            WorkExperience = WorkExperience,
            DesiredSalary = DesiredSalary,
            Skills = ProfileSkills?
                .Select(ps => ps.Skill)
                .Select(s => new SkillDto
                {
                    Id = s.Id,
                    Name = s.Name
                }),
            Locations = LocationProfiles?.Select(lp => lp.Location).Select(l => new LocationGetDto
            {
                Id = l.Id,
                City = l.City,
                Country = l.Country,
            })
        };
    }
}

public static class ProfileMapperExtension
{
    public static CandidateProfile MapCreateToCandidateProfile(this CandidateProfile profile, ProfileCreateDto profileCreateDto )
    {
        return new CandidateProfile
        {
            UserId = profileCreateDto.UserId,
            Name = profileCreateDto.Name,
            Surname = profileCreateDto.Surname,
            PositionTitle = profileCreateDto.PositionTitle,
            Email = profileCreateDto.Email,
            PhoneNumber = profileCreateDto.PhoneNumber,
            WorkExperience = Experience.NoExperience
        };
    }
    
    public static RecruiterProfile MapCreateToRecruiterProfile(this RecruiterProfile profile, ProfileCreateDto profileCreateDto)
    {
        return new RecruiterProfile
        {
            UserId = profileCreateDto.UserId,
            Name = profileCreateDto.Name,
            Surname = profileCreateDto.Surname,
            Email = profileCreateDto.Email,
            PhoneNumber = profileCreateDto.PhoneNumber,
        };
    }
}