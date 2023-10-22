using VacanciesService.Domain.Enums;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.DTO;

public class VacancyGetAllDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string PositionTitle { get; set; }
    public string Description { get; set; }
    public double Salary { get; set; }
    public Experience Experience { get; set; }
    public AttendanceMode AttendanceMode { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CompanyName { get; set; }
    public IEnumerable<LocationGetDto> Locations { get; set; }
}