using MediatR;

namespace WebApp.Data.CQS.Commands;

public class UpdateTextForArticlesCommand : IRequest //ICommand
{
    public Dictionary<Guid, string> Data { get; set; }
}