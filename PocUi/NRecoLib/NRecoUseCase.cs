namespace PocUi.NRecoLib;

public class NRecoUseCase(INRecoConverter converter)
{
    private readonly INRecoConverter _converter = converter;

    public async Task<byte[]> ExecuteAsync(string htmlContent)
    {
        Console.WriteLine("NRecoUseCase");
        return await _converter.GerarPdf(htmlContent);
    }
}
