using Microsoft.AspNetCore.Mvc;
using PocUi.Services;

namespace PocUi.Gotenberg;

public static class GotenbergEndpoint
{
    public static IEndpointRouteBuilder MapGotenberg(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Gotenberg",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] GotenbergUseCase useCase) =>
                {
                    Console.WriteLine("GotenbergEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html);
                    return Results.File(pdfBytes, "application/pdf", "Gotenberg.pdf");
                })
            .WithName("Gotenberg")
            .WithOpenApi();

        return endpoints;
    }
}
