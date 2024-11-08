using Microsoft.AspNetCore.Mvc;
using PocUi.Services;

namespace PocUi.PugPdfLib;

public static class PugPdfEndpoint
{
    /// <summary>
    /// Gerar Pdf utilizando PugPDF.
    /// Inclui na dll os binários do wkhtmltopdf. 
    /// Gerou pfd todo zoado.
    /// Não consegui rodar no Docker: 
    ///     System.Exception: 
    ///         /app/bin/Debug/net8.0/wkhtmltopdf/linux/x64/wkhtmltopdf: 
    ///         error while loading shared libraries: 
    ///         libQt5WebKitWidgets.so.5: 
    ///         cannot open shared object file: No such file or directory
    /// https://github.com/pug-pelle-p/pugpdf
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapPugPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/pugpdf",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] PugPdfUseCase useCase) =>
            {
                Console.WriteLine("PugPdfEndpoint");
                var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html);
                return Results.File(pdfBytes, "application/pdf", "PugPdf.pdf");

            })
            .WithName("PrintWithPugPDF")
            .WithOpenApi();

        return endpoints;
    }
}
