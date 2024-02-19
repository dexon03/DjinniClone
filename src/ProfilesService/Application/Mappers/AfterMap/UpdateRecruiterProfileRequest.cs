using AutoMapper;
using ProfilesService.Domain.DTO;
using ProfilesService.Domain.Models;

namespace ProfilesService.Application.Mappers.AfterMap;

public class UpdateRecruiterProfileRequest : IMappingAction<RecruiterProfileUpdateDto,RecruiterProfile>
{
    public void Process(RecruiterProfileUpdateDto source, RecruiterProfile destination, ResolutionContext context)
    {
        if(source.CompanyId != null && source.CompanyId != Guid.Empty && destination.CompanyId != source.CompanyId)
        {
            destination.CompanyId = source.CompanyId;
        }
    }
}