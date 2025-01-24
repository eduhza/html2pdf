using Microsoft.AspNetCore.Mvc;
using PocAPI.Services;

namespace PocAPI.Html2Pdf;

public static class Html2PdfEndpoint
{
    public static IEndpointRouteBuilder MapHtml2Pdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Html2Pdf",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] Html2PdfUseCase useCase,
                CancellationToken cancellationToken) =>
                {
                    Console.WriteLine("Html2PdfEndpoint");
                    var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html, cancellationToken);
                    try
                    {
                        return Results.File(pdfBytes, "application/pdf", "html2pdf.pdf");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return Results.BadRequest(ex);
                    }
                })
            .WithName("Html2Pdf")
            .WithOpenApi();

        return endpoints;
    }
}
