namespace PocAPI.Services;

public interface IHtmlToPdfService
{
    Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken = default);
}
