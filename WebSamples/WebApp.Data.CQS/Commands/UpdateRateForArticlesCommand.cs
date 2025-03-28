using MediatR;
using WebApp.Data.Entities;

namespace WebApp.Data.CQS.Commands;

public class UpdateRateForArticlesCommand : IRequest //ICommand
{
    public Dictionary<Guid, double> Data { get; set; }
}