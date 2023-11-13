using System.ComponentModel.DataAnnotations.Schema;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class RecruiterProfile : Profile
{
    [ForeignKey("Company")]
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }
}


public static class RecruiterProfileExtension
{
    public static RecruiterProfile MapUpdate(this RecruiterProfile profile, RecruiterProfileUpdateDto dto)
    {
        profile.Name = dto.Name;
        profile.Surname = dto.Surname;
        profile.PhoneNumber = dto.PhoneNumber;
        profile.CompanyId = dto.CompanyId;
        profile.PositionTitle = dto.PositionTitle;
        profile.Email = dto.Email;
        profile.Description = dto.Description;
        profile.ImageUrl = dto.ImageUrl;
        profile.GitHubUrl = dto.GitHubUrl;
        profile.LinkedInUrl = dto.LinkedInUrl;
        profile.DateBirth = dto.DateBirth;
        return profile;
    }
} 