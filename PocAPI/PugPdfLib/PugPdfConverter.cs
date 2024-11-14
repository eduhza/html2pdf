using System.Diagnostics;

namespace PocAPI.PugPdfLib;

/// <summary>
/// Gerar Pdf utilizando PugPDF.
/// Inclui na dll os binários do wkhtmltopdf (bin/). 
/// Não consegui rodar no Docker: 
///     System.Exception: 
///         /app/bin/Debug/net8.0/wkhtmltopdf/linux/x64/wkhtmltopdf: 
///         error while loading shared libraries: 
///         libQt5WebKitWidgets.so.5: 
///         cannot open shared object file: No such file or directory
/// https://github.com/pug-pelle-p/pugpdf
/// </summary>
/// <param name="endpoints"></param>
/// <returns></returns>
public class PugPdfConverter : IPugPdfConverter
{
    private PugPdf.Core.HtmlToPdf _renderer;

    public PugPdfConverter()
    {
        _renderer ??= new PugPdf.Core.HtmlToPdf();
    }

    public async Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("GERANDO PDF PugPdfConverter");

        _renderer ??= new PugPdf.Core.HtmlToPdf();

        var sw = new Stopwatch();
        sw.Start();
        var pdfDocument = await _renderer.RenderHtmlAsPdfAsync(htmlContent);
        byte[] bytes = pdfDocument.BinaryData;
        sw.Stop();
        Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
        return bytes;
    }
}
