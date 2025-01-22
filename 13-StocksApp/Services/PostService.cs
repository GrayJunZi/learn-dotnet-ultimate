using System.Text.Json;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.ServiceContracts;

namespace StocksApp.Services;

public class PostService : IPostService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TradingOptions _options;

    public PostService(IHttpClientFactory httpClientFactory,IOptions<TradingOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_options.Url),
            Headers = { },
        };

        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        var response = await streamReader.ReadToEndAsync();
        var data = JsonSerializer.Deserialize<IEnumerable<Post>>(response);
        return data;
    }
}