namespace PocAPI.iTextSharpLib;

public class ItextSharpUseCase(IItextSharpConverter converter)
{
    private readonly IItextSharpConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("ItextSharpUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
