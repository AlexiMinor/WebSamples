﻿namespace WebApp.Core.DTOs;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Content { get; set; }
    public string Url { get; set; }
    public string? ImageUrl { get; set; }
    public double? PositivityRate { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid SourceId { get; set; }
    public string SourceName { get; set; }
}