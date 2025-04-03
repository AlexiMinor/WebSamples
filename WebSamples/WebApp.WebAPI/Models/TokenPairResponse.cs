namespace WebApp.WebAPI.Models;

public class TokenPairResponse
{
    public string Jwt { get; set; }
    public string RefreshToken { get; set; }
}