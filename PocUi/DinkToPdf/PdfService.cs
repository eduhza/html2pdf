using DinkToPdf;
using DinkToPdf.Contracts;
using PocUi.Services;

namespace PocUi.DinkToPdf;

public class PdfService(IConverter converter) : IHtmlToPdfService
{
    private readonly IConverter _converter = converter;
    public Task<byte[]> GerarPdf(string htmlContent)
    {
        var outputPath = Path.Combine(Path.GetTempPath(), "DinkToPdf.pdf");
        var pdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
                Out = outputPath
            },
            Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
        };

        return Task.FromResult(_converter.Convert(pdfDocument));
    }
}
