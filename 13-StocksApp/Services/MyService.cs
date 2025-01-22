namespace StocksApp.Services;

public class MyService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MyService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task Run()
    {
        using var httpClient = _httpClientFactory.CreateClient();
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://jsonplaceholder.typicode.com/posts"),
            Headers = { },
        };
        
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        
        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        var response = await streamReader.ReadToEndAsync();
        Console.WriteLine(response);
    }
}