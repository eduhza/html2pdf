namespace PocUi.Services;

public interface IHtmlToPdfService
{
    Task<byte[]> GerarPdf(string htmlContent);
}
