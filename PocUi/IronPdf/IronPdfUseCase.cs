using PocUi.Services;

namespace PocUi.IronPdf;

public class IronPdfUseCase(IHtmlToPdfService converter)
{
    private readonly IHtmlToPdfService _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        return await _converter.GerarPdf(htmlContent);
    }
}
