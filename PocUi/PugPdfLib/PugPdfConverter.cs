namespace PocUi.PugPdfLib;

public class PugPdfConverter : IPugPdfConverter
{
    public async Task<byte[]> GerarPdf(string htmlContent)
    {
        Console.WriteLine("GERANDO PDF PugPdfConverter");
        var renderer = new PugPdf.Core.HtmlToPdf();
        var pdf = await renderer.RenderHtmlAsPdfAsync(htmlContent);
        return pdf.BinaryData;
    }
}
