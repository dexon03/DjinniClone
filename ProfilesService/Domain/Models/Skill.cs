

using System.Text.Json.Serialization;

namespace ProfilesService.Domain.Models;

public class Skill
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]public ICollection<ProfileSkills> ProfileSkills { get; set; }
}