﻿namespace ProfilesService.Domain.DTO;

public class UserUpdateDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string UserRole { get; set; }
}