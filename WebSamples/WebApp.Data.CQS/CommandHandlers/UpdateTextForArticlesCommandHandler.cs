using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.CQS.Commands;

namespace WebApp.Data.CQS.CommandHandlers;

public class UpdateTextForArticlesCommandHandler : IRequestHandler<UpdateTextForArticlesCommand>
{
    private readonly ArticleAggregatorContext _context;

    public UpdateTextForArticlesCommandHandler(ArticleAggregatorContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTextForArticlesCommand request, CancellationToken cancellationToken)
    {
        foreach (var keyValuePair in request.Data)
        {
            var article = await _context.Articles.SingleOrDefaultAsync(art => art.Id.Equals(keyValuePair.Key), 
                cancellationToken: cancellationToken);
            if (article != null)
            {
                article.Content = keyValuePair.Value;
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}