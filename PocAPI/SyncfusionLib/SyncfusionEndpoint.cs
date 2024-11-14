using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.SyncfusionLib
{
    public static class SyncfusionEndpoint
    {
        public static IEndpointRouteBuilder MapSyncfusion(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/syncfusion",
                async (
                    [FromServices] InvoiceFactory invoiceFactory,
                    [FromServices] SyncfusionUseCase useCase,
                    CancellationToken cancellationToken) =>
                {
                    Console.WriteLine("SyncfusionEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                    return Results.File(pdfBytes, "application/pdf", "IronPdf.pdf");
                })
                .WithName("syncfusion")
                .WithOpenApi();

            return endpoints;
        }
    }
}
