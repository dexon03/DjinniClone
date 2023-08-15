using AutoMapper;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.MapperProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VacancyUpdateDto, Vacancy>();
        CreateMap<VacancyCreateDto, Vacancy>();
        CreateMap<Skill, Skill>();
    }
}