using Riok.Mapperly.Abstractions;
using WebApp.Core.DTOs;
using WebApp.Data.Entities;
using WebApp.MVC.Models;

namespace WebApp.Services.Mappers;

[Mapper]
public partial class ArticleMapper
{
    [MapProperty($"{nameof(ArticleDto.PositivityRate)}", nameof(ArticleModel.Rate))]
    [MapProperty($"{nameof(ArticleDto.SourceName)}", nameof(ArticleModel.Source))]
    public partial ArticleModel ArticleDtoToArticleModel(ArticleDto article);

    public partial Article ArticleDtoToArticle(ArticleDto article);

    //[MapValue(nameof(ArticleDto.Id))]
    //[MapValue(nameof(ArticleDto.Content), "")]
    //[MapValue(nameof(ArticleDto.Url),"")]
    public partial ArticleDto AddArticleModelToArticleDto(AddArticleModel article);

    public partial ArticleModel ArticleToArticleModel(Article? article);


    public partial IQueryable<ArticleDto> QueryableProjectionToDto(IQueryable<Article> articles);

    [MapProperty($"{nameof(Article.Source)}.{nameof(Article.Source.Name)}", nameof(ArticleDto.SourceName))]
    public partial ArticleDto ArticleToArticleDto(Article? article);

}