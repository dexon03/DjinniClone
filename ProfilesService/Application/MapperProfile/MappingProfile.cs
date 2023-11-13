using AutoMapper;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;


namespace ProfilesService.Application.MapperProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LocationCreateDto, Location>();
        CreateMap<LocationUpdateDto, Location>();
    }
}