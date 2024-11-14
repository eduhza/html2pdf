namespace PocAPI.PugPdfLib;

public class PugPdfUseCase(IPugPdfConverter converter)
{
    private readonly IPugPdfConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("PugPdfUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
