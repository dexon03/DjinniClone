using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Enums;

namespace ProfilesService.Domain.Models.Common;

public class Profile<T>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
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

    public virtual IProfileDto<T> ToDto()
    {
        throw new NotImplementedException();
    }
}

public static class ProfileMapperExtension
{
    public static CandidateProfile MapCreateToCandidateProfile(this CandidateProfile profile,
        ProfileCreateDto profileCreateDto)
    {
        return new CandidateProfile
        {
            UserId = profileCreateDto.UserId,
            Name = profileCreateDto.Name,
            Surname = profileCreateDto.Surname,
            PositionTitle = profileCreateDto.PositionTitle,
            Email = profileCreateDto.Email,
            PhoneNumber = profileCreateDto.PhoneNumber,
            WorkExperience = Experience.NoExperience
        };
    }

    public static RecruiterProfile MapCreateToRecruiterProfile(this RecruiterProfile profile,
        ProfileCreateDto profileCreateDto)
    {
        return new RecruiterProfile
        {
            UserId = profileCreateDto.UserId,
            Name = profileCreateDto.Name,
            Surname = profileCreateDto.Surname,
            Email = profileCreateDto.Email,
            PhoneNumber = profileCreateDto.PhoneNumber,
        };
    }
}