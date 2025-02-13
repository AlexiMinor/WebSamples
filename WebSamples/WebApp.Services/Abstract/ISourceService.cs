using WebApp.Data.Entities;

namespace WebApp.Services.Abstract;

public interface ISourceService
{
    public Task<Source[]> GetSourceWithRssAsync();

}