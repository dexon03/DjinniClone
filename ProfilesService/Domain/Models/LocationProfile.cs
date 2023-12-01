namespace ProfilesService.Domain.Models;

public class LocationProfile
{
    public Guid ProfileId { get; set; }
    public Guid LocationId { get; set; }
    public virtual CandidateProfile Profile { get; set; }
    public virtual Location Location { get; set; }
}