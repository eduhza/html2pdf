using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.DinkToPdf;

public static class DinkToPdfEndpoint
{
    public static IEndpointRouteBuilder MapDinkToPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/DinkToPdf",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] DinkToPdfUseCase useCase,
                CancellationToken cancellationToken) =>
            {
                Console.WriteLine("DinkToPdfEndpoint");
                var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                return Results.File(pdfBytes, "application/pdf", "DinkToPdf.pdf");
            })
            .WithName("DinkToPdf")
            .WithOpenApi();

        return endpoints;
    }

}
