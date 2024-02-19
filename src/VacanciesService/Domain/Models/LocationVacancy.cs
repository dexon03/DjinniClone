namespace VacanciesService.Domain.Models;

public class LocationVacancy
{
    public Guid LocationId { get; set; }
    public Guid VacancyId { get; set; }
    public Location Location { get; set; } = null!;
    public Vacancy Vacancy { get; set; } = null!;
}