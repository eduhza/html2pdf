using Microsoft.AspNetCore.Mvc;
using Razor.Templating.Core;

namespace PocUi.Gotenberg;

public static class GotenbergEndpoint
{
    public static IEndpointRouteBuilder MapGotenberg(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Gotenberg",
            async (
                [FromServices] IRazorTemplateEngine _razorTemplateEngine,
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] GotenbergUseCase useCase) =>
                {
                    //Invoice invoice = invoiceFactory.Create();
                    var html = await _razorTemplateEngine.RenderAsync("Views/PaginaModelo.cshtml", invoiceFactory.invoice);

                    var path = Path.Combine(Path.GetTempPath(), "Gotenberg.pdf");

                    var pdfBytes = await useCase.ExecuteAsync(html);
                    //await File.WriteAllBytesAsync(path, pdfBytes);
                    var base64Pdf = Convert.ToBase64String(pdfBytes);

                    return Results.Ok(base64Pdf);
                })
            .WithName("Gotenberg")
            .WithOpenApi();

        return endpoints;
    }
}
