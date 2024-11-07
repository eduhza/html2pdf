using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;

namespace PocUi.IronPdf;

public static class IronPdfEndpoint
{
    public static IEndpointRouteBuilder MapIronPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/ironpdf",
            async (
                [FromServices] IRazorTemplateEngine _razorTemplateEngine,
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] IronPdfUseCase useCase) =>
                {
                    var html = await _razorTemplateEngine.RenderAsync("Views/PaginaModelo.cshtml", invoiceFactory.invoice);
                    var path = Path.Combine(Path.GetTempPath(), "IronPdf.pdf");

                    var pdfBytes = await useCase.ExecuteAsync(html);
                    //await File.WriteAllBytesAsync(path, pdfBytes);
                    var base64Pdf = Convert.ToBase64String(pdfBytes);
                    return Results.Ok(base64Pdf);

                    //return Results.File(
                    //    pdfBytes,
                    //    "application/pdf",
                    //    $"invoice-{invoiceFactory.invoice.Number}.pdf");
                })
            .WithName("ironpdf")
            .WithOpenApi();

        return endpoints;
    }
}
