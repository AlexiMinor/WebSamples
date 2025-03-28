using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using WebApp.Core.DTOs;
using WebApp.Data.Entities;
using WebApp.Services.Abstract;
using WebApp.Services.Mappers;

namespace WebApp.Services.Implementations;

public class RssService : IRssService
{
    private readonly ArticleMapper _articleMapper;

    public RssService(ArticleMapper articleMapper)
    {
        _articleMapper = articleMapper;
    }

    public async Task<Article[]> GetRssDataAsync(string rssUrl, Guid rssId, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(rssUrl))
        {
            throw new ArgumentNullException(nameof(rssUrl));
        }
    
        using (var xmlReader = XmlReader.Create(rssUrl))
        {
            var syndicationFeed = SyndicationFeed.Load(xmlReader);

            var articles = syndicationFeed.Items
                .Select(item => GetArticleFromSyndicationItem(item, rssId))
                .ToArray();
            return articles;
        }
    }

    private Article GetArticleFromSyndicationItem(SyndicationItem item, Guid sourceId)
    {
        var (imageUrl, content) = GetImageUrlAndContent(item);
        var article = new Article()
        {
            Id = Guid.NewGuid(),
            Title = item.Title.Text,
            Description = content,
            CreationDate = item.PublishDate.UtcDateTime,
            Url = item.Id,
            ImageUrl = imageUrl,
            SourceId = sourceId
        };
        return article;
    }


    private (string, string) GetImageUrlAndContent(SyndicationItem item)
    {
        var content = item.Summary;
        var imageUrl = string.Empty;
        var text = new StringBuilder();

        if (content != null)
        {
            var match = Regex.Match(content.Text, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                imageUrl = match.Groups[1].Value;
            }
            var textMatches = Regex.Matches(content.Text, @"<p>(?:(?!<\/p>).)*<\/p>", RegexOptions.Singleline);

                foreach (Match textMatch in textMatches.SkipLast(1).Reverse().SkipLast(1).Reverse())
                {
                    text.AppendLine(textMatch.Value);
                }
        }
        return (imageUrl, text.ToString());
    }
}