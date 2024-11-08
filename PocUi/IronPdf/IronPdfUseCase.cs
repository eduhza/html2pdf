namespace PocUi.IronPdf;

public class IronPdfUseCase(IIronPdfConverter converter)
{
    private readonly IIronPdfConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        Console.WriteLine("IronPdfUseCase");
        return await _converter.GerarPdf(htmlContent);
    }
}
