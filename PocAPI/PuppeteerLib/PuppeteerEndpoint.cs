using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.PuppeteerLib;

public static class PuppeteerEndpoint
{
    public static IEndpointRouteBuilder MapPuppeteer(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/puppeteer",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] PuppeteerUseCase useCase,
                CancellationToken cancellationToken) =>
                {
                    Console.WriteLine("PuppeteerEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                    return Results.File(pdfBytes, "application/pdf", "Puppeteer.pdf");
                })
            .WithName("puppeteer")
            .WithOpenApi();

        return endpoints;
    }
}
