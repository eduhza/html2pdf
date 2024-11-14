namespace PocAPI.DinkToPdf;

public class DinkToPdfUseCase(IIDinkToPdfConverter converter)
{
    private readonly IIDinkToPdfConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("DinkToPdfUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
