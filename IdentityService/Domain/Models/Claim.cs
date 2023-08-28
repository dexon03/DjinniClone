using System.Text.Json.Serialization;

namespace IdentityService.Domain.Models;

public class Claim
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    [JsonIgnore]public virtual ICollection<RoleClaim> RoleClaim { get; set; }
}