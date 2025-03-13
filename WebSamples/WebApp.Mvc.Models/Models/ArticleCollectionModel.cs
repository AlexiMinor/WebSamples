using WebApp.Mvc.Models.Models;

namespace WebApp.MVC.Models;

public class ArticleCollectionModel
{
    public ArticleModel[] Articles { get; set; }
    public PageInfo PageInfo { get; set; }
}