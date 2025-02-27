using MediatR;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers;

public class AddArticleCommandHandler : IRequestHandler<AddArticleCommand>
{
    private readonly ArticleAggregatorContext _context;

    public AddArticleCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        await _context.Articles.AddAsync(request.Article, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}