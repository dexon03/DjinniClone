using VacanciesService.Domain.Enums;
using VacanciesService.Domain.Models;

namespace VacanciesService.Domain.DTO;

public record VacancyGetDto
{
    public Guid Id { get; set; }
    public Guid RecruiterId { get; set; }
    public string? Title { get; set; }
    public string? PositionTitle { get; set; }
    public string? Description { get; set; }
    public AttendanceMode? AttendanceMode { get; set; }
    public Experience? Experience { get; set; }
    public double? Salary { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Company Company { get; set; }
    public Category Category { get; set; }
    public IEnumerable<Location>? Locations { get; set; }
    public IEnumerable<Skill>? Skills { get; set; }
};