namespace PocUi.PuppeteerLib;

public class PuppeteerUseCase(IPuppeteerConverter converter)
{
    private readonly IPuppeteerConverter _converter = converter;
    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        Console.WriteLine("PuppeteerUseCase");
        return await _converter.GerarPdf(htmlContent);
    }
}
