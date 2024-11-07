using Microsoft.AspNetCore.Mvc;
using PugPdf.Core;
using Razor.Templating.Core;

namespace PocUi.PugPdfLib;

public static class PugPdfEndpoint
{
    /// <summary>
    /// Gerar Pdf utilizando PugPDF.
    /// Inclui na dll os binários do wkhtmltopdf. 
    /// Gerou pfd todo zoado.
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
    public static IEndpointRouteBuilder MapPugPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/pugpdf",
            async (
                [FromServices] IRazorTemplateEngine _razorTemplateEngine,
                [FromServices] InvoiceFactory invoiceFactory) =>
                {
                    Console.WriteLine("Gerando PDF com PugPDF...");
                    var renderer = new PugPdf.Core.HtmlToPdf();
                    renderer.PrintOptions.Title = "PDF gerado com PugPDF";
                    renderer.PrintOptions.Header = new PdfHeader()
                    {
                        CenterText = "Texto central",
                        DisplayLine = true
                    };

                    var html = await _razorTemplateEngine.RenderAsync("Views/PaginaModelo.cshtml", invoiceFactory.invoice);
                    var pdf = await renderer.RenderHtmlAsPdfAsync(html);

                    var path = Path.Combine(Path.GetTempPath(), "pugpdf.pdf");
                    pdf.SaveAs(path);
                    Console.WriteLine($"Arquivo gerado: {path}");

                    return Task.CompletedTask;
                })
            .WithName("PrintWithPugPDF")
            .WithOpenApi();

        return endpoints;
    }
}
