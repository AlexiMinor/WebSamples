﻿namespace WebApp.Core.DTOs;

public class LoginDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Role { get; set; }
}