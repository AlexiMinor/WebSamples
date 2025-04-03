using MediatR;

namespace WebApp.Data.CQS.Commands;

public class RemoveRefreshTokenCommand : IRequest
{
    public Guid RefreshToken { get; set; }
}