namespace PocAPI.GotenbergLib;

public class GotenbergUseCase(IGoternbergConverter converter)
{
    private readonly IGoternbergConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("GotenbergUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
