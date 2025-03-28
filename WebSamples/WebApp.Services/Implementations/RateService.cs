using System.Net.Http.Json;
using System.Text;
using System.Web.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApp.Services.Abstract;

namespace WebApp.Services.Implementations;

public class RateService : IRateService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<RateService> _logger;
    
    public RateService(IConfiguration configuration, ILogger<RateService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public async Task<double?> GetRateAsync(string preparedText, CancellationToken cancellationToken = default)
    {
        var keywordsDictionary = _configuration.GetSection("KeywordsDictionary")
            .GetChildren()
            .ToDictionary(sect => sect.Key, sect => Convert.ToInt32(sect.Value));
        
        var lemmas = await GetLemmasAsync(preparedText, cancellationToken);
        
        double? rate = null;

        foreach (var lemma in lemmas)
        {
            if (keywordsDictionary.TryGetValue(lemma, out var rateValue))
            {
                if (rate == null)
                {
                    rate = rateValue;
                }
                else
                {
                    rate += rateValue;
                }
            }
        }

        if (rate.HasValue)
        {
            var finalRate = rate / lemmas.Length * 100;
            return finalRate;
        }
        else
        {
            return null;
        }
    }

    private async Task<string[]> GetLemmasAsync(string text, CancellationToken cancellationToken)
    {
        var url = string.Format(_configuration["Lemmatizer:BaseUrl"], _configuration["Lemmatizer:ApiKey"]);
        
        using (var httpClient = new HttpClient())
        {
            //var request = CreateRequest(url, text);

            //var response = await httpClient.SendAsync(request, cancellationToken);
            //if (response.IsSuccessStatusCode)
            //{

            //    //var lemmas = await response.Content.ReadFromJsonAsync<string[]>(cancellationToken);
            //    //return lemmas;
            //}
            //else
            //{
            //    _logger.LogError($"Failed to get lemmas from {url}. StatusCode:{response.StatusCode}");
            //    _logger.LogWarning("Trying to get from reserved:");
                var reservedUrl = _configuration["Lemmatizer:ReservedUrl"];
                var requestForReserve = CreateRequest(reservedUrl, text);
                
                var responseForReserve = await httpClient.SendAsync(requestForReserve, cancellationToken);
                if (responseForReserve.IsSuccessStatusCode)
                {
                    var responseString = await responseForReserve.Content.ReadAsStringAsync(cancellationToken);
                    var lemmas = JsonConvert.DeserializeObject<TexterraLemmatizationResponse[]>(responseString)?.
                            FirstOrDefault()?
                            .Annotations
                            .Lemma
                            .Select(lemma => lemma.Value)
                            .Where(s => !string.IsNullOrWhiteSpace(s))
                            .ToArray();
                    return lemmas;
                }
                else
                {
                    _logger.LogError($"Failed to get lemmas from {url} and {reservedUrl}");
                    throw new Exception($"Failed to get lemmas from {url} and {reservedUrl}");
                }
            //}
        }
    }
    
    private HttpRequestMessage CreateRequest(string url, string text)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Accept", "application/json");
        //request.Headers.Add("Content-Type", "application/json");
        request.Content = JsonContent.Create(new[]
        {
            new {Text = text}
        });
        return request;
    }
    
    
    
    public class TexterraLemmatizationResponse
    {
        public string Text { get; set; }
        public Annotations Annotations { get;set; }
    }
    
    public class Annotations
    {
        public Lemma[] Lemma { get; set; }
    }
    
    public class Lemma
    {
        public int Start { get; set; }
        public int End { get; set; }
        public string Value { get; set; }
    }
}