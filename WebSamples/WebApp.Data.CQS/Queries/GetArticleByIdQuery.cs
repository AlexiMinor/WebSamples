using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetArticleByIdQuery : IRequest<Article?> //IQuery<Guid[]>
{
   public Guid Id { get; set; }
}