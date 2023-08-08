using System.ComponentModel.DataAnnotations.Schema;

namespace ProfilesService.Models;

public class Profile
{
    public Guid Id { get; set; }
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PositionTitle { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public DateOnly DateBirth { get; set; }
    public double WorkExperience { get; set; }
    public double DesiredSalary { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<ProfileSkills> ProfileSkills { get; set; }
    public virtual ICollection<LocationProfile> LocationProfiles { get; set; }
}