using VacanciesService.Domain.Enums;

namespace VacanciesService.Domain.DTO;

public class VacancyUpdateDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string PositionTitle { get; set; }
    public string Description { get; set; }
    public double Salary { get; set; }
    public bool IsActive { get; set; }
    public Experience Experience { get; set; }
    public AttendanceMode AttendanceMode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Guid> LocationIds { get; set; }
    public List<Guid> SkillIds { get; set; }
}