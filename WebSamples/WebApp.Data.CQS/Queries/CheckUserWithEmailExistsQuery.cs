using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class CheckUserWithEmailExistsQuery : IRequest<bool> //IQuery<Guid[]>
{
   public string Email { get; set; }
}