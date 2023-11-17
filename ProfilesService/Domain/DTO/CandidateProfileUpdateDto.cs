using ProfilesService.Domain.Enums;
using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.DTO;

public class CandidateProfileUpdateDto : Profile<CandidateProfile>
{
    public string PositionTitle { get; set; }
    public Experience WorkExperience { get; set; }
    public double DesiredSalary { get; set; }
    public List<Guid> SkillIds { get; set; }
    public List<Guid> LocationIds { get; set; }
}
