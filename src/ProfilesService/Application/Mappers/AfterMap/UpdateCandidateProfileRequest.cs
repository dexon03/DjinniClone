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
            ProfileId = destination.Id
        })?.ToList();

        var profileLocations = source.Locations?.Select(l => new LocationProfile
        {
            LocationId = l.Id,
            ProfileId = destination.Id
        })?.ToList();
        
        
        var existsProfileSkills = _repository.GetAll<ProfileSkills>()
            .Where(ps => ps.ProfileId == destination.Id)
            .ToList();

        var existsProfileLocations = _repository.GetAll<LocationProfile>()
            .Where(lp => lp.ProfileId == destination.Id)
            .ToList();
        
        if (profileSkills != null)
        {
            var skillsToInsert = profileSkills
                .Where(ps => existsProfileSkills.All(eps => eps.SkillId != ps.SkillId));
            _repository.CreateRange(skillsToInsert.ToArray());
            var skillsToRemove = existsProfileSkills
                .Where(eps => profileSkills.All(ps => ps.SkillId != eps.SkillId));
            _repository.DeleteRange(skillsToRemove.ToArray());
        }

        if(profileLocations != null)
        {
            var locToInsert = profileLocations
                .Where(lp => existsProfileLocations.All(epl => epl.LocationId != lp.LocationId));
            _repository.CreateRange(locToInsert.ToArray());
            var locToRemove = existsProfileLocations
                .Where(epl => profileLocations.All(lp => lp.LocationId != epl.LocationId));
            _repository.DeleteRange(locToRemove.ToArray());
        }
    }
}