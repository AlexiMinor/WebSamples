using Riok.Mapperly.Abstractions;
using WebApp.Core.DTOs;
using WebApp.Data;
using WebApp.Data.Entities;
using WebApp.MVC.Models;

namespace WebApp.MVC.Mappers;

[Mapper]
public partial class ArticleMapper
{
    public partial ArticleModel ArticleModelToArticle(Article article);

    [MapProperty($"{nameof(ArticleDto.PositivityRate)}", nameof(ArticleModel.Rate))]
    [MapProperty($"{nameof(ArticleDto.SourceName)}", nameof(ArticleModel.Source))]
    public partial ArticleModel ArticleDtoToArticleModel(ArticleDto article);
    
    public partial Article ArticleDtoToArticle(ArticleDto article);

    //[MapValue(nameof(ArticleDto.Id))]
    //[MapValue(nameof(ArticleDto.Content), "")]
    //[MapValue(nameof(ArticleDto.Url),"")]
    public partial ArticleDto AddArticleModelToArticleDto(AddArticleModel article);


    public partial IQueryable<ArticleDto> QueryableProjectionToDto(IQueryable<Article> articles);

    [MapProperty($"{nameof(Article.Source)}.{nameof(Article.Source.Name)}", nameof(ArticleDto.SourceName))]
    public partial ArticleDto ArticleToArticleDto(Article article);

}