using AutoMapper;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.MapperProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vacancy, Vacancy>();
        CreateMap<Skill, Skill>();
    }
}