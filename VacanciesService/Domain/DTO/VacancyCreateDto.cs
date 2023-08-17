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
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<Guid> LocationIds { get; set; }
    public List<Guid> SkillIds { get; set; }
}