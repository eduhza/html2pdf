using PocUi.Services;
using PuppeteerSharp;

namespace PocUi.PuppeteerLib;

public class PuppeteerConverter : IHtmlToPdfService
{
    public async Task<byte[]> GerarPdf(string htmlContent)
    {
        try
        {
            await new BrowserFetcher().DownloadAsync();

            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions
                {
                    Headless = true
                });
            await using var page = await browser.NewPageAsync();
            await page.SetContentAsync(htmlContent);

            var pdfStream = await page.PdfStreamAsync();

            using var memoryStream = new MemoryStream();
            await pdfStream.CopyToAsync(memoryStream);
            byte[] byteArray = memoryStream.ToArray();

            return byteArray;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }
}
