using MediatR;

namespace WebApp.Data.CQS.Queries;

public class GetIdsForArticlesWithNoTextQuery : IRequest<Guid[]> //IQuery<Guid[]>
{
   
}