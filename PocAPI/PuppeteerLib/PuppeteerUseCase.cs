namespace PocAPI.PuppeteerLib;

public class PuppeteerUseCase(IPuppeteerConverter converter)
{
    private readonly IPuppeteerConverter _converter = converter;
    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("PuppeteerUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
