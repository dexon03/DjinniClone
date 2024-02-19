using AutoMapper;
using VacanciesService.Application.MapperProfile.AfterMap;
using VacanciesService.Domain.DTO;
using VacanciesService.Domain.Models;

namespace VacanciesService.Application.MapperProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VacancyUpdateDto, Vacancy>().AfterMap<VacancyUpdateRequest>();
        CreateMap<VacancyCreateDto, Vacancy>().AfterMap<VacancyCreateRequest>();
        CreateMap<CompanyCreateDto, Company>();
        CreateMap<CompanyUpdateDto, Company>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();
        CreateMap<LocationCreateDto, Location>();
        CreateMap<LocationUpdateDto, Location>();
        CreateMap<SkillCreateDto, Skill>();
        CreateMap<SkillUpdateDto, Skill>();
    }
}