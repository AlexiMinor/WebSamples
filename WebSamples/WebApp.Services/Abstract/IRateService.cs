namespace WebApp.Services.Abstract;

public interface IRateService
{
    public Task<double?> GetRateAsync(string preparedText, CancellationToken cancellationToken = default);
}