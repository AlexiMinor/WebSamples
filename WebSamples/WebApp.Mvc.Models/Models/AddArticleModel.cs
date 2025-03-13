using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;

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
    
    public Guid SourceId { get; set; }
    
    public SelectList Sources { get; set; }
}