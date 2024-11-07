namespace PocUi.Services;

public class GoternbergConverter(HttpClient httpClient) : IHtmlToPdfService
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<byte[]> GerarPdf(string htmlContent)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(htmlContent), "files", "index.html");

        var response = await _httpClient.PostAsync("/forms/chromium/convert/html", content);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
