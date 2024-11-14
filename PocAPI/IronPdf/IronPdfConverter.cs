using System.Diagnostics;

namespace PocAPI.IronPdf;

public class IronPdfConverter(ChromePdfRenderer renderer) : IIronPdfConverter
{
    private ChromePdfRenderer _renderer = renderer;

    public async Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken)
    {
        _renderer ??= new ChromePdfRenderer();

        Console.WriteLine("GERANDO PDF IronPdfConverter");
        License.LicenseKey = "IRONSUITE.EDUARDOARRUDA.MIGRATE.INFO.28696-917FA71F08-P5KV2-I73BVCHBE2IL-WSX6MOKM4LAJ-4FEL5LCWP2BQ-UBQXQYPRSDYT-PMCX47D7ME23-IARLIMZ7HKBT-YXITTD-TQ3E524Q7FCOEA-DEPLOYMENT.TRIAL-MCGQHZ.TRIAL.EXPIRES.07.DEC.2024";

        var sw = new Stopwatch();
        sw.Start();
        var pdfDocument = await _renderer.RenderHtmlAsPdfAsync(htmlContent);
        byte[] bytes = pdfDocument.BinaryData;
        sw.Stop();
        Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
        return bytes;
    }
}
