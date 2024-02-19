using System.Text.Json.Serialization;
using ProfilesService.Domain.Enums;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class CandidateProfile : Profile<CandidateProfile>
{
    public string? GitHubUrl { get; set; }
    public Experience WorkExperience { get; set; } = Experience.NoExperience;
    public double DesiredSalary { get; set; } = 0;
    public AttendanceMode Attendance { get; set; } = AttendanceMode.Remote;
    [JsonIgnore]public ICollection<ProfileSkills>? ProfileSkills { get; set; }
    [JsonIgnore]public ICollection<LocationProfile>? LocationProfiles { get; set; }
}