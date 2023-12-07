using VacanciesService.Domain.Enums;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.DTO;

public class VacancyCreateDto
{
    public Guid CategoryId { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; }
    public string PositionTitle { get; set; }
    public string Description { get; set; }
    public double Salary { get; set; }
    public AttendanceMode Attendance { get; set; }
    public Experience Experience { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Location> Locations { get; set; }
    public List<Skill> Skills { get; set; }
}