using Microsoft.AspNetCore.Mvc;
using PocUi.Services;

namespace PocUi.IronPdf;

public static class IronPdfEndpoint
{
    public static IEndpointRouteBuilder MapIronPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/ironpdf",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] IronPdfUseCase useCase) =>
                {
                    Console.WriteLine("IronPdfEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html);
                    return Results.File(pdfBytes, "application/pdf", "IronPdf.pdf");
                })
            .WithName("ironpdf")
            .WithOpenApi();

        return endpoints;
    }
}
