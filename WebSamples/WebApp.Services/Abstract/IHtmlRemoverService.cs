namespace WebApp.Services.Abstract;

public interface IHtmlRemoverService
{
    public string RemoveHtmlTags(string rawText, CancellationToken cancellationToken = default);
   
}