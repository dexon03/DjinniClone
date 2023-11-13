namespace ProfilesService.Domain.Models;

public class ProfileSkills
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Guid SkillId { get; set; }
    public virtual CandidateProfile Profile { get; set; }
    public virtual Skill Skill { get; set; }
}