using System.ComponentModel.DataAnnotations;

namespace WebApp.MVC.Models;

public class AddArticleModel
{
    [Required]
    [MinLength(3)]
    public string Title { get; set; }
    [Required]
    [MinLength(10)]
    public string Description { get; set; }

    [Range(-10, 10)]
    public double Rate { get; set; }
}