using PocUi.Services;

namespace PocUi.IronPdf;

public class IronPdfConverter() : IHtmlToPdfService
{
    private readonly ChromePdfRenderer _renderer = new();
    public Task<byte[]> GerarPdf(string htmlContent)
    {
        var pdfDocument = _renderer.RenderHtmlAsPdf(htmlContent);
        return Task.FromResult(pdfDocument.BinaryData);
    }
}
