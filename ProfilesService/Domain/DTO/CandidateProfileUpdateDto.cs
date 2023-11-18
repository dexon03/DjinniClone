using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.Enums;
using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.DTO;

public class CandidateProfileUpdateDto : IProfileUpdateDto<CandidateProfile>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly DateBirth { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? PositionTitle { get; set; }
    public bool IsActive { get; set; } = false;
    public Experience WorkExperience { get; set; }
    public double DesiredSalary { get; set; }
    public List<Guid> SkillIds { get; set; }
    public List<Guid> LocationIds { get; set; }
}
