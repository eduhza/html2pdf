namespace PocUi.Gotenberg;

public class GotenbergUseCase(IGoternbergConverter converter)
{
    private readonly IGoternbergConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        Console.WriteLine("GotenbergUseCase");
        return await _converter.GerarPdf(htmlContent);
    }
}
