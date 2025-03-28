using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers;

public class UpdateRateForArticlesCommandHandler : IRequestHandler<UpdateRateForArticlesCommand>
{
    private readonly ArticleAggregatorContext _context;

    public UpdateRateForArticlesCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateRateForArticlesCommand request, CancellationToken cancellationToken)
    {
        foreach (var kvp in request.Data)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(x => x.Id == kvp.Key, cancellationToken);
            if (article != null)
            {
                article.PositivityRate = kvp.Value;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}