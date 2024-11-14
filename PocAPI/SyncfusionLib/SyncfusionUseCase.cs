namespace PocAPI.SyncfusionLib;

public class SyncfusionUseCase(ISyncfusionConverter converter)
{
    private readonly ISyncfusionConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("SyncfusionUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
