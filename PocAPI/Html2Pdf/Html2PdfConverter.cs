using html2pdflib = Html2Pdf.Lib;

namespace PocAPI.Html2Pdf
{
    public class Html2PdfConverter : IHtml2PdfConverter
    {
        public Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken = default)
        {
            //log provider for console app
            ILoggerFactory factory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Debug);
            });
            ILogger logger = factory.CreateLogger("Program");

            Console.WriteLine("GERANDO PDF Html2PdfConverter");
            var arguments = new html2pdflib.Arguments()
                .SetPageSize(html2pdflib.PageSize.A4)
                .SetTitle("Html2PdfConverter")
                .SetPageOrientation(html2pdflib.PageOrientation.Portrait)
                .SetPageMargins(5, 5, 5, 5)
                .SetHeaderText("This is a header text", html2pdflib.TextAlignment.Center, "Verdana", 15)
                .DoNotPrintBackground()
                .WithNoPdfCompression()
                .AddLogger(logger)
                .TimeoutConvert(10000);

            try
            {
                var resultConvert = html2pdflib.Converter.FromHtml(htmlContent, arguments);
                var elapsedtimeConvert = resultConvert.Elapsedtime;
                if (resultConvert.HasValue)
                {
                    var bytespdf = resultConvert.Content!;
                    //await File.WriteAllBytesAsync($"html2pdf.pdf", bytespdf, cancellationToken);
                    return Task.FromResult(bytespdf);
                }
                else
                {
                    //Erro See resultConvert.Error
                    var erro = resultConvert.Error!;
                    Console.WriteLine(erro);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return Task.FromResult(Array.Empty<byte>());
        }
    }
}
