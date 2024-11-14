namespace PocAPI.NRecoLib;

public class NRecoUseCase(INRecoConverter converter)
{
    private readonly INRecoConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("NRecoUseCase");
        return await _converter.GerarPdf(htmlContent, cancellationToken);
    }
}
