namespace PocUi.IronPdf;

public class IronPdfConverter() : IIronPdfConverter
{
    private readonly ChromePdfRenderer _renderer = new();
    public async Task<byte[]> GerarPdf(string htmlContent)
    {
        Console.WriteLine("GERANDO PDF IronPdfConverter");
        var pdfDocument = await _renderer.RenderHtmlAsPdfAsync(htmlContent);

        return pdfDocument.BinaryData;
    }
}
