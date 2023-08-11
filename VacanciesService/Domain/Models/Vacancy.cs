﻿using System.ComponentModel.DataAnnotations.Schema;

namespace VacanciesService.Domain.Models;

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
    public double Salary { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Category Category { get; set; }
    public Company Company { get; set; }
    public virtual ICollection<LocationVacancy> LocationVacancies { get; set; }
    public virtual ICollection<VacancySkill> VacancySkills { get; set; }
}