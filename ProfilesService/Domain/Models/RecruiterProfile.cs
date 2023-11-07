using System.ComponentModel.DataAnnotations.Schema;
using ProfilesService.Domain.Models.Common;

namespace ProfilesService.Domain.Models;

public class RecruiterProfile : Profile
{
    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}