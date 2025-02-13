using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Queries;

public class GetPositiveArticlesWithPaginationQuery : IRequest<IQueryable<Article>> //IQuery
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public double? PositivityRate { get; set; }
}