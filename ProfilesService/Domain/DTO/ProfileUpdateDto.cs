namespace ProfilesService.Domain.DTO;

public class ProfileUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PositionTitle { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string GitHubUrl { get; set; }
    public string LinkedInUrl { get; set; }
    public DateOnly DateBirth { get; set; }
    public double WorkExperience { get; set; }
    public double DesiredSalary { get; set; }
    public List<Guid> SkillIds { get; set; }
    public List<Guid> LocationIds { get; set; }
}
