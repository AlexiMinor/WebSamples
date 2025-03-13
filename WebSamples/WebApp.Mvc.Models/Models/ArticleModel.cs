namespace WebApp.Mvc.Models.Models;

public class ArticleModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public string Source { get; set; }
    public double Rate { get; set; }
    public string Text { get; set; }
}