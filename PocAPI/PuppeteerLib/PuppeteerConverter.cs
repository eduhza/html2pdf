using PuppeteerSharp;
using System.Diagnostics;
using System.Threading.Channels;

namespace PocAPI.PuppeteerLib;

/// <summary>
/// https://www.puppeteersharp.com/
/// Puppeteer is a JavaScript library which provides a high-level API to control Chrome or Firefox over the DevTools Protocol or WebDriver BiDi. 
/// Puppeteer runs in the headless (no visible UI) by default
/// Funciona no windows e docker
/// </summary>
public class PuppeteerConverter : IPuppeteerConverter
{
    private static IBrowser? _browser;
    public PuppeteerConverter()
    {
        // Inicializa o browser de forma assíncrona ao instanciar a classe, se ainda não estiver inicializado
        if (_browser == null)
        {
            var launchTask = InitializeBrowserAsync();
            launchTask.Wait(); // Espera até que o browser seja inicializado
        }
    }
    private async Task InitializeBrowserAsync()
    {
        //await new BrowserFetcher().DownloadAsync();
        _browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
    }

    public async Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken = default)
    {
        try
        {
            Console.WriteLine("GERANDO PDF PuppeteerConverter");
            if (_browser == null)
            {
                await InitializeBrowserAsync(); // Garante que o browser foi inicializado
            }

            var sw = new Stopwatch();
            sw.Start();
            byte[] bytes;
            //bytes = await ConvertToPdf(htmlContent);
            bytes = await ExecutarChannelAsync(htmlContent, cancellationToken); // Teste de execução em paralelo
            sw.Stop();
            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            return bytes;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    private async Task<byte[]> ConvertToPdf(string htmlContent)
    {
        await using var page = await _browser.NewPageAsync();
        await page.SetContentAsync(htmlContent);

        var bytes = await page.PdfDataAsync(new PdfOptions { PrintBackground = true });

        return bytes;
    }

    private async Task<byte[]> ExecutarChannelAsync(string htmlContent, CancellationToken cancellationToken)
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
                    await writer.WriteAsync(() => ConvertToPdf(htmlContent));
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
