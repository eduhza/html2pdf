using Microsoft.AspNetCore.Mvc;
using PocUi.Services;

namespace PocUi.NRecoLib;

public static class NRecoEndpoint
{
    public static IEndpointRouteBuilder MapNReco(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/nreco",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] NRecoUseCase useCase) =>
                {
                    Console.WriteLine("NRecoEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html);
                    return Results.File(pdfBytes, "application/pdf", "NReco.pdf");
                })
            .WithName("nreco")
            .WithOpenApi();

        return endpoints;
    }
}
