﻿using System.Text.Json.Serialization;

namespace IdentityService.Domain.Models;

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    [JsonIgnore]public virtual ICollection<RoleClaim> RoleClaim { get; set; }
}