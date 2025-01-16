﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.MVC.Models;

public class ArticleModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDate { get; set; }
    public string Source { get; set; }
    public double Rate { get; set; }
}