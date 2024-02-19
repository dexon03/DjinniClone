namespace ProfilesService.Domain.DTO;

public class UserCreateDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string UserRole { get; set; }
}