using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetArticlesWithoutRateQuery : IRequest<Article[]> 
{
}