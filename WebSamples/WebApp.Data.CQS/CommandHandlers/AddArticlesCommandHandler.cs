using MediatR;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers;

public class AddArticlesCommandHandler : IRequestHandler<AddArticlesCommand>
{
    private readonly ArticleAggregatorContext _context;

    public AddArticlesCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(AddArticlesCommand request, CancellationToken cancellationToken)
    {
        await _context.Articles.AddRangeAsync(request.Articles, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}