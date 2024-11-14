using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.NRecoLib;

public static class NRecoEndpoint
{
    public static IEndpointRouteBuilder MapNReco(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/nreco",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] NRecoUseCase useCase,
                CancellationToken cancellationToken) =>
                {
                    Console.WriteLine("NRecoEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                    return Results.File(pdfBytes, "application/pdf", "NReco.pdf");
                })
            .WithName("nreco")
            .WithOpenApi();

        return endpoints;
    }
}
