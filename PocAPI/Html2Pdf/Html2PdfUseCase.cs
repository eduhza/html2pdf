namespace PocAPI.Html2Pdf;

public class Html2PdfUseCase(IHtml2PdfConverter converter)
{
    private readonly IHtml2PdfConverter _converter = converter;
    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("Html2PdfUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
