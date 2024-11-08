using Microsoft.AspNetCore.Mvc;
using PocUi.Services;

namespace PocUi.PuppeteerLib;

public static class PuppeteerEndpoint
{
    public static IEndpointRouteBuilder MapPuppeteer(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/puppeteer",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] PuppeteerUseCase useCase) =>
                {
                    Console.WriteLine("PuppeteerEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html);
                    return Results.File(pdfBytes, "application/pdf", "Puppeteer.pdf");
                })
            .WithName("puppeteer")
            .WithOpenApi();

        return endpoints;
    }
}
