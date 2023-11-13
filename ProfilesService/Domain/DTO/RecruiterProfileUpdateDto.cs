using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.DTO;

public class RecruiterProfileUpdateDto : Profile
{
    public Guid CompanyId { get; set; }
};