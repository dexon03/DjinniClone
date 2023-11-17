using ProfilesService.Domain.Models;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.DTO;

public class RecruiterProfileUpdateDto : Profile<RecruiterProfile>
{
    public Guid CompanyId { get; set; }
};