﻿using AutoMapper.Configuration.Annotations;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.Enums;
using ProfilesService.Domain.Models;

namespace ProfilesService.Domain.DTO;

public class CandidateProfileUpdateDto : IProfileUpdateDto<CandidateProfile>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly DateBirth { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? GitHubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
    public string? PositionTitle { get; set; }
    public bool IsActive { get; set; } = false;
    public Experience WorkExperience { get; set; }
    public double DesiredSalary { get; set; }
    public AttendanceMode Attendance { get; set; }
    [Ignore]
    public IFormFile? PdfResume { get; set; }
    public IEnumerable<SkillDto>? Skills { get; set; }
    public IEnumerable<LocationGetDto>? Locations { get; set; }
}
