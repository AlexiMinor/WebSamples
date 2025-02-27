using MediatR;

namespace WebApp.Data.CQS.Queries;

public class GetArticlesCountByPositivityRateQuery : IRequest<int> //IQuery<Guid[]>
{
   public double PositivityRate { get; set; }
}