namespace WebApp.WebAPI.Models;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; }
}