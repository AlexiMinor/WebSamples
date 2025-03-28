using System.Text.RegularExpressions;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations;

public class HtmlRemoverService : IHtmlRemoverService
{
    public string RemoveHtmlTags(string rawText, CancellationToken cancellationToken = default)
    {
        return Regex.Replace(rawText, "<.*?>", string.Empty);
    }
}