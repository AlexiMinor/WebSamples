using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Commands;

public class AddArticlesCommand : IRequest //ICommand
{
    public IEnumerable<Article> Articles { get; set; }
}