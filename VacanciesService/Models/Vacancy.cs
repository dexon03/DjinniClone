using System.ComponentModel.DataAnnotations.Schema;

namespace VacanciesService.Models;

public class Vacancy
{
    public Guid Id { get; set; }
    [ForeignKey("Category")]
    public Guid CategoryId { get; set; }
    [ForeignKey("Company")]
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
    public virtual ICollection<LocationVacancy> Locations { get; set; }
    public virtual ICollection<VacancySkill> Skills { get; set; }
}