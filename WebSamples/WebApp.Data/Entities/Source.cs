namespace WebApp.Data.Entities;

public class Source
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string OriginalUrl { get; set; }
    public string RssUrl { get; set; }

    public ICollection<Article> Articles { get; set; }
}