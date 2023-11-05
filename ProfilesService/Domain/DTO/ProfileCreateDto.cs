namespace ProfilesService.Domain.DTO;

public class ProfileCreateDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PositionTitle { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public ProfileRole Role { get; set; }
}