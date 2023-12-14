namespace VacanciesService.Domain.DTO;

public record LocationDto
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
};
