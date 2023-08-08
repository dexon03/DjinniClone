namespace ProfilesService.Models;

public class LocationProfile
{
    public Guid Id { get; set; }
    public Guid ProfileId { get; set; }
    public Guid LocationId { get; set; }
    public virtual Profile Profile { get; set; }
    public virtual Location Location { get; set; }
}