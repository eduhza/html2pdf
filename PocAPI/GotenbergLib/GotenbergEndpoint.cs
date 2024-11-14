using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.GotenbergLib;

public static class GotenbergEndpoint
{
    public static IEndpointRouteBuilder MapGotenberg(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Gotenberg",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] GotenbergUseCase useCase,
                CancellationToken cancellationToken) =>
                {
                    try
                    {

                        Console.WriteLine("GotenbergEndpoint");
                        var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                        return Results.File(pdfBytes, "application/pdf", "Gotenberg.pdf");
                    }
                    catch (OperationCanceledException)
                    {
                        return Results.StatusCode(StatusCodes.Status499ClientClosedRequest);
                    }
                })
            .WithName("Gotenberg")
            .WithOpenApi();

        return endpoints;
    }
}
