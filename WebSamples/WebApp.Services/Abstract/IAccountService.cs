using WebApp.Core.DTOs;

namespace WebApp.Services.Abstract;

public interface IAccountService
{
    Task<LoginDto?> TryToLogin(string modelEmail, string modelPassword);
    Task<LoginDto?> TryToRegister(string modelEmail, string modelPassword);
}