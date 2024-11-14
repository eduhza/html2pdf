using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.IronPdf;

public static class IronPdfEndpoint
{
    public static IEndpointRouteBuilder MapIronPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/ironpdf",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] IronPdfUseCase useCase,
                CancellationToken cancellationToken) =>
                {
                    Console.WriteLine("IronPdfEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                    return Results.File(pdfBytes, "application/pdf", "IronPdf.pdf");
                })
            .WithName("ironpdf")
            .WithOpenApi();

        return endpoints;
    }
}
