using PocUi.Gotenberg;

namespace PocUi.Services;

public class GoternbergConverter(HttpClient httpClient) : IGoternbergConverter
{
    private readonly HttpClient _httpClient = httpClient;
    public async Task<byte[]> GerarPdf(string htmlContent)
    {
        Console.WriteLine("GERANDO PDF GoternbergConverter");
        var content = new MultipartFormDataContent();
        content.Headers.Add("Gotenberg-Output-Filename", "my_filename");
        content.Headers.Add("Gotenberg-Trace", "debug");
        content.Add(new StringContent(htmlContent), "files", "index.html");
        var response = await _httpClient.PostAsync("/forms/chromium/convert/html", content);

        //content.Add(new StringContent("https://ironpdf.com/blog/compare-to-other-components/nreco-net-core-html-to-pdf-alternatives/"), "url");
        //var response = await _httpClient.PostAsync("/forms/chromium/convert/url", content);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsByteArrayAsync();
    }
}
