using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Commands;

public class AddArticleCommand : IRequest //ICommand
{
    public Article Article { get; set; }
}