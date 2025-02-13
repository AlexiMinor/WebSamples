
using System.ComponentModel.DataAnnotations;

namespace WebApp.MVC.Models;

public class PaginationModel
{
    //[Required]
    [Range(1,Int32.MaxValue)]
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 15;
}