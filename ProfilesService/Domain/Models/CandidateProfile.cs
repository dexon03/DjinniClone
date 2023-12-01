using System.Text.Json.Serialization;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Enums;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class CandidateProfile : Profile<CandidateProfile>
{
    public string? GitHubUrl { get; set; }
    public Experience WorkExperience { get; set; } = Experience.NoExperience;
    public double DesiredSalary { get; set; } = 0;
    [JsonIgnore]public ICollection<ProfileSkills>? ProfileSkills { get; set; }
    [JsonIgnore]public ICollection<LocationProfile>? LocationProfiles { get; set; }
    public override GetCandidateProfileDto ToDto()
    {
        return new GetCandidateProfileDto
        {
            Id = Id,
            Name = Name,
            Surname = Surname,
            Email = Email ?? String.Empty,
            PhoneNumber = PhoneNumber ?? String.Empty,
            DateBirth = DateBirth,
            Description = Description ?? String.Empty,
            ImageUrl = ImageUrl ?? String.Empty,
            GitHubUrl = GitHubUrl ?? String.Empty,
            LinkedInUrl = LinkedInUrl ?? String.Empty,
            PositionTitle = PositionTitle ?? String.Empty,
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

