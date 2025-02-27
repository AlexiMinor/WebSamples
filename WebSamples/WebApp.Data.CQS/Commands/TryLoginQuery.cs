using MediatR;

namespace WebApp.Data.CQS.Commands;

public class TryToRegisterUserCommand : IRequest
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}