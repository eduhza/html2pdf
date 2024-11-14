using System.Diagnostics;
using System.Threading.Channels;

namespace PocAPI.GotenbergLib;

public class GoternbergConverter(HttpClient httpClient) : IGoternbergConverter
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("GERANDO PDF GoternbergConverter");

        var content = new MultipartFormDataContent();
        content.Headers.Add("Gotenberg-Output-Filename", "my_filename");
        content.Headers.Add("Gotenberg-Trace", "debug");
        content.Add(new StringContent(htmlContent), "files", "index.html");

        var sw = new Stopwatch();
        sw.Start();
        byte[] bytes;
        bytes = await PostAndGetBytesAsync(content);
        //bytes = await ExecutarChannelAsync(content, cancellationToken); // Teste de execução em paralelo
        sw.Stop();
        Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");

        return bytes;
    }

    private async Task<byte[]> PostAndGetBytesAsync(MultipartFormDataContent content, int tentativasRemanescentes = 3)
    {
        var response = await _httpClient.PostAsync("/forms/chromium/convert/html", content);
        if (!response.IsSuccessStatusCode)
        {
            tentativasRemanescentes--;
            if (tentativasRemanescentes > 0)
            {
                await PostAndGetBytesAsync(content, tentativasRemanescentes);
            }
            else
            {
                Console.WriteLine("********** FALHOU AO CRIAR PDF **********");
                return [];
            }
        }
        //response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }

    private async Task<byte[]> ExecutarChannelAsync(MultipartFormDataContent content, CancellationToken cancellationToken)
    {
        const int maxConcurrentRequests = 1000;
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
                    await writer.WriteAsync(() => PostAndGetBytesAsync(content));
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
