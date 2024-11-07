using PocUi.Services;
using Razor.Templating.Core;

namespace PocUi.DinkToPdf;

public static class DinkToPdfEndpoint
{
    /// <summary>
    /// Gerar Pdf utilizando DinkToPdf.
    /// binários do wkhtmltopdf incluído na pasta do projeto.
    /// No windows, PDF ficou ok.
    /// Não consegui rodar no docker, tentei as soluções no git mas não funcionou.
    /// https://github.com/rdvojmoc/DinkToPdf/issues/179
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapDinkToPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/DinkToPdf",
            async (
                IRazorTemplateEngine _razorTemplateEngine,
                InvoiceFactory invoiceFactory,
                IHtmlToPdfService pdfService) =>
                {
                    //Invoice invoice = invoiceFactory.Create();
                    Console.WriteLine("Gerando PaginaModelo (razor) em html...");
                    var html = await _razorTemplateEngine.RenderAsync("Views/PaginaModelo.cshtml", invoiceFactory.invoice);

                    var path = Path.Combine(Path.GetTempPath(), "DinkToPdf.pdf");
                    var pdfBytes = await pdfService.GerarPdf(html);
                    await File.WriteAllBytesAsync(path, pdfBytes);

                    Console.WriteLine(path);
                    return Results.Ok(path);
                })
            .WithName("DinkToPdf")
            .WithOpenApi();

        return endpoints;
    }

}
