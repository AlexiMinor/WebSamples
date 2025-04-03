using MediatR;

namespace WebApp.Data.CQS.Commands;

public class CreateRefreshTokenCommand : IRequest //ICommand
{
    public Guid RefreshToken { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpireAt { get; set; }
}