using System.Net.Http;
using System.Security.Cryptography;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using WebApp.Data.Entities;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations;

public class RssService : IRssService
{
    public async Task<Article[]> GetRssDataAsync(Source rss, CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(rss.RssUrl))
        {
            throw new ArgumentNullException(nameof(rss.RssUrl));
        }
    
        using (var xmlReader = XmlReader.Create(rss.RssUrl))
        {
            var syndicationFeed = SyndicationFeed.Load(xmlReader);

            var articles = syndicationFeed.Items
                .Select(item => GetArticleFromSyndicationItem(item, rss.Id)).ToArray();
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

/*
 * <p>
 * <a href="https://money.onliner.by/2025/02/06/perevod-iz-za-granicy">
 * <img src="https://content.onliner.by/news/thumbnail/ca62ddf0ce891b231970152539779c9f.jpg" alt="" />
 * </a>
 * </p>
 * <p>
 * Читатель Максим на днях получил международный перевод за рекламу на YouTube. За него надо было заплатить комиссию, которой он не ожидал.
 * </p>
 * <p>
 * <a href="https://money.onliner.by/2025/02/06/perevod-iz-za-granicy">Читать далее…</a>
 * </p>
 */