namespace PocUi.PugPdfLib;

public class PugPdfUseCase(IPugPdfConverter converter)
{
    private readonly IPugPdfConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        Console.WriteLine("PugPdfUseCase");
        return await _converter.GerarPdf(htmlContent);
    }
}
