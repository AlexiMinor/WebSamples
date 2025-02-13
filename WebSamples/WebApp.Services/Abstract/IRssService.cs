using WebApp.Data.Entities;

namespace WebApp.Services.Abstract;

public interface IRssService
{
    public Task<Article[]> GetRssDataAsync(Source rss, CancellationToken token = default);

}