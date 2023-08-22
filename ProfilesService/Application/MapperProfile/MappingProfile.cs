using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;
using Profile = AutoMapper.Profile;

namespace ProfilesService.Application.MapperProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LocationCreateDto, Location>();
        CreateMap<LocationUpdateDto, Location>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
        CreateMap<ProfileCreateDto, Profile>();
        CreateMap<ProfileUpdateDto, Profile>();
    }
}