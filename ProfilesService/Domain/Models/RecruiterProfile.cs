using System.ComponentModel.DataAnnotations.Schema;
using ProfilesService.Domain.Contracts;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class RecruiterProfile : Profile<RecruiterProfile>
{
    [ForeignKey("Company")]
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }
    public override GetRecruiterProfileDto ToDto()
    {
        return new GetRecruiterProfileDto
        {
            Id = Id,
            Name = Name,
            Surname = Surname,
            Email = Email ?? String.Empty,
            PhoneNumber = PhoneNumber ?? String.Empty,
            DateBirth = DateBirth,
            Description = Description ?? String.Empty,
            ImageUrl = ImageUrl ?? String.Empty,
            LinkedInUrl = LinkedInUrl ?? String.Empty,
            PositionTitle = PositionTitle ?? String.Empty,
            IsActive = IsActive,
            Company = Company 
        };
    }
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
        profile.LinkedInUrl = dto.LinkedInUrl;
        profile.DateBirth = dto.DateBirth;
        return profile;
    }
} 