using PocUi.Services;

namespace PocUi.Gotenberg;

public class GotenbergUseCase(IHtmlToPdfService converter)
{
    private readonly IHtmlToPdfService _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        return await _converter.GerarPdf(htmlContent);
    }
}
