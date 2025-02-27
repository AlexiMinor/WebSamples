using MediatR;
using WebApp.Core.DTOs;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetUserByEmailQuery : IRequest<User>
{
    public string Email { get; set; }
}