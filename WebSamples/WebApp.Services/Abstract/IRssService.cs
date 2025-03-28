using WebApp.Core.DTOs;
using WebApp.Data.Entities;

namespace WebApp.Services.Abstract;

public interface IRssService
{
    public Task<Article[]> GetRssDataAsync(string rssUrl, Guid rssId, CancellationToken token = default);

}