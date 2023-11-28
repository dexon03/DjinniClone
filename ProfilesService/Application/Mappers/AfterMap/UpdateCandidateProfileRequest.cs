using AutoMapper;
using Core.Database;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.Mappers.AfterMap;

public class UpdateCandidateProfileRequest : IMappingAction<CandidateProfileUpdateDto, CandidateProfile>
{
    private readonly IRepository _repository;

    public UpdateCandidateProfileRequest(IRepository repository)
    {
        _repository = repository;
    }
    
    public void Process(CandidateProfileUpdateDto source, CandidateProfile destination, ResolutionContext context)
    {
        var profileSkills = source.Skills?.Select(s => new ProfileSkills
        {
            SkillId = s.Id,
            ProfileId = source.Id
        }).ToArray();
        
        var profileLocations = source.Locations?.Select(l => new LocationProfile
        {
            LocationId = l.Id,
            ProfileId = source.Id
        }).ToArray();

        if (!(profileSkills is null))
        {
            _repository.CreateRange<ProfileSkills>(profileSkills);
            destination.ProfileSkills = profileSkills;
        }
        if(!(profileLocations is null))
        {
            _repository.CreateRange<LocationProfile>(profileLocations);
            destination.LocationProfiles = profileLocations;
        }
    }
}