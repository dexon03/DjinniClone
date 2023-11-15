using System.Text.Json.Serialization;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Enums;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class CandidateProfile : Profile
{
    public Experience WorkExperience { get; set; }
    public double DesiredSalary{ get; set; }
    [JsonIgnore]public ICollection<ProfileSkills> ProfileSkills { get; set; }
    [JsonIgnore]public ICollection<LocationProfile> LocationProfiles { get; set; }
}

public static class ProfileMapperExtension
{
    public static CandidateProfile MapCreateToCandidateProfile(this CandidateProfile profile, ProfileCreateDto profileCreateDto )
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
    
    public static RecruiterProfile MapCreateToRecruiterProfile(this RecruiterProfile profile, ProfileCreateDto profileCreateDto)
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