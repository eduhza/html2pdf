namespace PocAPI.IronPdf
{
    public class IronPdfUseCase(IIronPdfConverter converter)
    {
        private readonly IIronPdfConverter _converter = converter;

        public async Task<byte[]> ExecuteAsync(string htmlContent,
                CancellationToken cancellationToken)
        {
            Console.WriteLine("IronPdfUseCase");
            return await _converter.GerarPdf(htmlContent, cancellationToken);
        }
    }
}
