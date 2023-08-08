namespace VacanciesService.Models;

public class LocationVacancy
{
    public Guid Id { get; set; }
    public Guid LocationId { get; set; }
    public Guid VacancyId { get; set; }
    public virtual Location Location { get; set; } = null!;
    public virtual Vacancy Vacancy { get; set; } = null!;
}