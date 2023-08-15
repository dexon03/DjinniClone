using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.DTO;

public class VacancyDTO
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid CompanyId { get; set; }
    public string Title { get; set; }
    public string PositionTitle { get; set; }
    public string Description { get; set; }
    public string Salary { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Category Category { get; set; }
    public Company Company { get; set; }
    public List<Location> Locations { get; set; }
    public List<Skill> Skills { get; set; }
}