using Microsoft.AspNetCore.Mvc;
using PocUi.Services;

namespace PocUi.DinkToPdf;

public static class DinkToPdfEndpoint
{
    /// <summary>
    /// Gerar Pdf utilizando DinkToPdf.
    /// binários do wkhtmltopdf incluído na pasta do projeto.
    /// No windows, PDF ficou ok.
    /// Não consegui rodar no docker, tentei as soluções no git mas não funcionou.
    /// https://github.com/rdvojmoc/DinkToPdf/issues/179
    /// </summary>
    /// <param name="endpoints"></param>
    /// <returns></returns>
    public static IEndpointRouteBuilder MapDinkToPdf(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/DinkToPdf",
            async (
                [FromServices] InvoiceFactory invoiceFactory,
                [FromServices] DinkToPdfUseCase useCase) =>
            {
                Console.WriteLine("DinkToPdfEndpoint");
                var pdfBytes = await useCase.ExecuteAsync(invoiceFactory.Html);
                return Results.File(pdfBytes, "application/pdf", "DinkToPdf.pdf");
            })
            .WithName("DinkToPdf")
            .WithOpenApi();

        return endpoints;
    }

}
