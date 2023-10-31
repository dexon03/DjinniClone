namespace VacanciesService.Domain.Models;

public class Location
{
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public ICollection<LocationVacancy> LocationVacancy { get; set; }
}