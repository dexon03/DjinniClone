﻿namespace IdentityService.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role? Role { get; set; }
}