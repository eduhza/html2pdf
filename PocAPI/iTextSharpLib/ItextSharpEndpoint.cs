using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.iTextSharpLib;

public static class ItextSharpEndpoint
{
    public static IEndpointRouteBuilder MapItextSharp(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/itextsharp",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] ItextSharpUseCase useCase,
                CancellationToken cancellationToken) =>
            {
                Console.WriteLine("ItextSharpEndpoint");
                var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", "ItextSharp.pdf");
            })
            .WithName("itextsharp")
            .WithOpenApi();

        return endpoints;
    }
}
