using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.PugPdfLib;

public static class PugPdfEndpoint
{
    public static IEndpointRouteBuilder MapPugPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/pugpdf",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] PugPdfUseCase useCase,
                CancellationToken cancellation) =>
            {
                Console.WriteLine("PugPdfEndpoint");
                var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellation);
                return Results.File(pdfBytes, "application/pdf", "PugPdf.pdf");

            })
            .WithName("PrintWithPugPDF")
            .WithOpenApi();

        return endpoints;
    }
}
