using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.MVC.Models;

public class RegistrationModel
{
    [Required]
    [EmailAddress]
    [Remote("CheckEmail", "Sample")] //only if client validation enabled
    public string Email { get; set; }
    
    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessage = "Passwords don't match")] //works for server & client validation
    public string PasswordConfirmation { get; set; }
}