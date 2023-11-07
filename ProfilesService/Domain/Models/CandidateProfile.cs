using System.Text.Json.Serialization;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class CandidateProfile : Profile
{
    public double WorkExperience { get; set; }
    public double DesiredSalary { get; set; }
    [JsonIgnore]public ICollection<ProfileSkills> ProfileSkills { get; set; }
    [JsonIgnore]public ICollection<LocationProfile> LocationProfiles { get; set; }
}

public static class ProfileMapperExtension
{
    public static CandidateProfile MapCreateToCandidateProfile(this Profile profile)
    {
        return new CandidateProfile
        {
            UserId = profile.UserId,
            Name = profile.Name,
            Surname = profile.Surname,
            PositionTitle = profile.PositionTitle,
            Email = profile.Email,
            PhoneNumber = profile.PhoneNumber,
        };
    }
    
    public static RecruiterProfile MapCreateToRecruiterProfile(this Profile profile)
    {
        return new RecruiterProfile
        {
            UserId = profile.UserId,
            Name = profile.Name,
            Surname = profile.Surname,
            Email = profile.Email,
            PhoneNumber = profile.PhoneNumber,
        };
    }
}