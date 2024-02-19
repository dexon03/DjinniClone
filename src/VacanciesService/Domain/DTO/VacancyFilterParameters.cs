using VacanciesService.Domain.Enums;

namespace VacanciesService.Domain.DTO;

public record VacancyFilterParameters
{
    public string? searchTerm { get; set; }
    public int page { get; set; }
    public int pageSize { get; set; }
    public Experience? experience { get; set; }
    public AttendanceMode? attendanceMode { get; set; }
    public Guid? skill { get; set; }
    public Guid? category { get; set; }
    public Guid? location { get; set; }
}