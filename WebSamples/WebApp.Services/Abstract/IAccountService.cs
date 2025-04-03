using WebApp.Core.DTOs;

namespace WebApp.Services.Abstract;

public interface IAccountService
{
    Task<LoginDto?> TryToLoginAsync(string modelEmail, string modelPassword);
    Task<LoginDto?> TryToRegister(string modelEmail, string modelPassword);
}