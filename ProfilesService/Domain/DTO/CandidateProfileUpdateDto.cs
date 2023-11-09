using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.DTO;

public class CandidateProfileUpdateDto : Profile
{
    public string PositionTitle { get; set; }
    public double WorkExperience { get; set; }
    public double DesiredSalary { get; set; }
    public List<Guid> SkillIds { get; set; }
    public List<Guid> LocationIds { get; set; }
}
