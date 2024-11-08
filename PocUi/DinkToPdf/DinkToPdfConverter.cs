using DinkToPdf;
using DinkToPdf.Contracts;

namespace PocUi.DinkToPdf;

public class DinkToPdfConverter(IConverter converter) : IIDinkToPdfConverter
{
    private readonly IConverter _converter = converter;
    public Task<byte[]> GerarPdf(string htmlContent)
    {
        var pdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait
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

        byte[] pdfBytes = _converter.Convert(pdfDocument);
        return Task.FromResult(pdfBytes);
    }
}
