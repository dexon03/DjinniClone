namespace ProfilesService.Domain.DTO;

public class LocationUpdateDto
{
    public Guid Id { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}