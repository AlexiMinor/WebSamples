using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Commands;

public class RevokeTokenCommand : IRequest //ICommand
{
    public Guid TokenId { get; set; }
}