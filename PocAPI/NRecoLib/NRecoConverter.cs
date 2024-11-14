using NReco.PdfGenerator;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace PocAPI.NRecoLib;

/// <summary>
/// https://www.nrecosite.com/pdf_generator_net.aspx
/// https://github.com/vipinc007/HtmlToPdfGenerator/tree/master
/// </summary>
public class NRecoConverter() : INRecoConverter
{
    public async Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken)
    {
        Console.WriteLine("GERANDO PDF NRecoConverter");
        string DemoLicenseKey = "pjfsL9eBhU5mER7ULRNO/pgqeXBsJF15ea8d+vpzJ/ja8LpELgrs2FoaZwNLRmsJpgwKahfzSvyCZDHZsI0Bs9KCaby6CXo02YxpA3iiwJPaDdfO+vTK/JCsjH/d9l6118KUVjegNFAraGSKA3Q6tMTg4/injiZ9wZ3kcjoXPjs=";

        var htmlToPdf = new HtmlToPdfConverter();
        //htmlToPdf.Quiet = false;
        //htmlToPdf.LogReceived += (sender, e) =>
        //{
        //    Console.WriteLine("WkHtmlToPdf Log: {0}", e.Data);
        //};

        htmlToPdf.License.SetLicenseKey("DEMO", DemoLicenseKey);


        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var pdfToolPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NRecoLib", "wkhtmltopdf");
            Console.WriteLine(pdfToolPath);
            htmlToPdf.WkHtmlToPdfExeName = "wkhtmltopdf.exe";
            htmlToPdf.PdfToolPath = pdfToolPath;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) // for Linux/OS-X: "wkhtmltopdf"
        {
            htmlToPdf.WkHtmlToPdfExeName = "wkhtmltopdf";
            htmlToPdf.PdfToolPath = "/usr/local/bin";
        }
        else
        {
            throw new ApplicationException("Plataforma não suportada");
        }

        var sw = new Stopwatch();
        sw.Start();
        byte[] bytes;
        bytes = htmlToPdf.GeneratePdf(htmlContent);
        //htmlToPdf.BeginBatch();
        //try
        //{
        //    bytes = await ExecutarChannelAsync(htmlToPdf, htmlContent, cancellationToken); // Teste de execução em paralelo
        //}
        //finally
        //{
        //    htmlToPdf.EndBatch();
        //}
        sw.Stop();
        Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
        return bytes;
    }

    private Task<byte[]> PostAndGetBytesAsync(HtmlToPdfConverter htmlToPdf, string htmlContent)
    {
        var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
        return Task.FromResult(pdfBytes);
    }

    private async Task<byte[]> ExecutarChannelAsync(HtmlToPdfConverter htmlToPdf, string htmlContent, CancellationToken cancellationToken)
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
                    await writer.WriteAsync(() => PostAndGetBytesAsync(htmlToPdf, htmlContent));
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
