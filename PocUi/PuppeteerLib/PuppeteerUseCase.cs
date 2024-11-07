using PocUi.Services;

namespace PocUi.PuppeteerLib;

public class PuppeteerUseCase(IHtmlToPdfService converter)
{
    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        return await converter.GerarPdf(htmlContent);
    }
}
