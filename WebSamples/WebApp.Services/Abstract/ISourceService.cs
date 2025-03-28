using WebApp.Data.Entities;

namespace WebApp.Services.Abstract;

public interface ISourceService
{
    public Task<Source?[]> GetSourceWithRssAsync(CancellationToken cancellationToken=default);
    public Task<Source?> GetByIdAsync(Guid id, CancellationToken cancellationToken=default);

}