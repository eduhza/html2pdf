using DinkToPdf;
using DinkToPdf.Contracts;
using System.Diagnostics;
using System.Threading.Channels;

namespace PocAPI.DinkToPdf;

/// <summary>
/// Gerar Pdf utilizando DinkToPdf.
/// binários do wkhtmltopdf incluído na pasta raiz do projeto (libwkhtmltox.dll/libwkhtmltox.dylib/libwkhtmltox.so).
/// No windows e Docker, PDF ficou ok.
/// </summary>
/// <param name="endpoints"></param>
/// <returns></returns>
public class DinkToPdfConverter(IConverter converter) : IIDinkToPdfConverter
{
    private readonly IConverter _converter = converter;
    public Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("GERANDO PDF DinkToPdfConverter");
        var pdfDocument = new HtmlToPdfDocument
        {
            GlobalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
            },
            Objects =
            {
                new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
        };

        var sw = new Stopwatch();
        sw.Start();
        byte[] bytes;
        bytes = _converter.Convert(pdfDocument);
        //bytes = await ExecutarChannelAsync(pdfDocument, cancellationToken); // Teste de execução em paralelo
        sw.Stop();
        Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");

        return Task.FromResult(bytes);
    }

    private Task<byte[]> Convert(HtmlToPdfDocument pdfDocument)
    {
        return Task.FromResult(_converter.Convert(pdfDocument));
    }


    private async Task<byte[]> ExecutarChannelAsync(HtmlToPdfDocument pdfDocument, CancellationToken cancellationToken)
    {
        const int maxConcurrentRequests = 100;
        const int totalRequests = 1000;
        var results = new List<byte[]>();

        // Cria um canal com limite de requisições concorrentes
        var channel = Channel.CreateBounded<Func<Task<byte[]>>>(maxConcurrentRequests);

        // Escreve as requisições no canal
        var writer = channel.Writer;
        _ = Task.Run(async () =>
        {
            try
            {
                for (int i = 0; i < totalRequests; i++)
                {
                    Console.WriteLine($"iteracao {i}");
                    cancellationToken.ThrowIfCancellationRequested();
                    await writer.WriteAsync(() => Convert(pdfDocument));
                }
            }
            finally
            {
                writer.Complete();
            }
        }, cancellationToken);

        // Executa as requisições concorrentemente
        var reader = channel.Reader;
        var consumers = Enumerable.Range(0, maxConcurrentRequests).Select(async _ =>
        {
            while (await reader.WaitToReadAsync())
            {
                while (reader.TryRead(out var taskFunc))
                {
                    // Executa a tarefa e adiciona o resultado à lista
                    var result = await taskFunc();
                    results.Add(result);
                }
            }
        });
        await Task.WhenAll(consumers);

        return results.FirstOrDefault();
    }
}
