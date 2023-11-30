using AutoMapper;
using ProfilesService.Application.Mappers.AfterMap;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CandidateProfileUpdateDto, CandidateProfile>().AfterMap<UpdateCandidateProfileRequest>();
        CreateMap<RecruiterProfileUpdateDto, RecruiterProfile>();
        CreateMap<CompanyCreateDto, Company>();
        CreateMap<CompanyUpdateDto, Company>();
    }
}