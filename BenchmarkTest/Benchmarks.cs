using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using DinkToPdf;
using PocAPI.DinkToPdf;
using PocAPI.GotenbergLib;
using PocAPI.Html2Pdf;

namespace BenchmarkTest;

public class PdfBenchmarks
{
    public readonly string _htmlContent;
    private readonly HttpClient _httpClient;
    private readonly DinkToPdfUseCase _dinkToPdf;
    private readonly GotenbergUseCase _gotenberg;
    //private readonly IronPdfUseCase _ironPdf;
    //private readonly NRecoUseCase _nReco;
    //private readonly PugPdfUseCase _pugPdf;
    //private readonly PuppeteerUseCase _puppeteer;
    private readonly Html2PdfUseCase _html2Pdf;
    private readonly CancellationToken cancellationToken = new CancellationToken();

    public PdfBenchmarks()
    {
        _htmlContent = GetHtml.htmlContent;
        _httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:3000") };
        _dinkToPdf = new DinkToPdfUseCase(new DinkToPdfConverter(new SynchronizedConverter(new PdfTools())));
        _gotenberg = new GotenbergUseCase(new GoternbergConverter(_httpClient));
        //_ironPdf = new IronPdfUseCase(new IronPdfConverter());
        //_nReco = new NRecoUseCase(new NRecoConverter());
        //_pugPdf = new PugPdfUseCase(new PugPdfConverter());
        //_puppeteer = new PuppeteerUseCase(new PuppeteerConverter());
        _html2Pdf = new Html2PdfUseCase(new Html2PdfConverter());
    }

    [Benchmark]
    public async Task BenchmarkDinkToPdf() => await _dinkToPdf.ExecuteAsync(_htmlContent, cancellationToken);

    [Benchmark]
    public async Task BenchmarkGotenberg() => await _gotenberg.ExecuteAsync(_htmlContent, cancellationToken);

    //[Benchmark]
    //public async Task BenchmarkIronPdf() => await _ironPdf.ExecuteAsync(_htmlContent);

    //[Benchmark]
    //public async Task BenchmarkNReco() => await _nReco.ExecuteAsync(_htmlContent, cancellationToken);

    //[Benchmark]
    //public async Task BenchmarkPugPdf() => await _pugPdf.ExecuteAsync(_htmlContent, cancellationToken);

    //[Benchmark]
    //public async Task BenchmarkPuppeteer() => await _puppeteer.ExecuteAsync(_htmlContent, cancellationToken);

    [Benchmark]
    public async Task BenchmarkHtml2Pdf() => await _html2Pdf.ExecuteAsync(_htmlContent, cancellationToken);


    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ManualConfig()
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                .AddValidator(JitOptimizationsValidator.DontFailOnError)
                .AddLogger(ConsoleLogger.Default)
                .AddColumnProvider(DefaultColumnProviders.Instance);

            await Task.Run(() =>
            {
                BenchmarkSwitcher
                    .FromAssembly(typeof(Program).Assembly)
                    .Run(args, config);
            });
        }
    }
}

internal static class GetHtml
{
    public static string htmlContent = "<!DOCTYPE html>\r\n<html lang=\"pt-BR\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Fatura</title>\r\n    <style>\r\n        body { font-family: Arial, sans-serif; margin: 0; padding: 0; }\r\n        .container { width: 80%; margin: 20px auto; border: 1px solid #ddd; padding: 20px; }\r\n        .header, .footer { text-align: center; margin-bottom: 20px; }\r\n        .header h1 { margin: 0; }\r\n        .section { margin-bottom: 20px; }\r\n        .section h2 { margin: 0 0 10px 0; font-size: 1.2em; }\r\n        table { width: 100%; border-collapse: collapse; margin-top: 10px; }\r\n        th, td { padding: 10px; border: 1px solid #ddd; text-align: left; }\r\n        th { background-color: #f4f4f4; }\r\n        .total { font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<div class=\"container\">\r\n    <!-- Header da Fatura -->\r\n    <div class=\"header\">\r\n        <h1>Fatura</h1>\r\n        <p><strong>Número da Fatura:</strong> 123456</p>\r\n        <p><strong>Data de Emissão:</strong> 2024-11-01</p>\r\n        <p><strong>Data de Vencimento:</strong> 2024-11-15</p>\r\n    </div>\r\n\r\n    <!-- Informações do Fornecedor -->\r\n    <div class=\"section\">\r\n        <h2>Fornecedor</h2>\r\n        <p><strong>Nome:</strong> Empresa Exemplo Ltda.</p>\r\n        <p><strong>Endereço:</strong> Rua Exemplo, 123 - São Paulo, SP</p>\r\n        <p><strong>Telefone:</strong> (11) 1234-5678</p>\r\n        <p><strong>Email:</strong> contato@empresaexemplo.com</p>\r\n    </div>\r\n\r\n    <!-- Informações do Cliente -->\r\n    <div class=\"section\">\r\n        <h2>Cliente</h2>\r\n        <p><strong>Nome:</strong> João Silva</p>\r\n        <p><strong>Endereço:</strong> Avenida Central, 456 - Rio de Janeiro, RJ</p>\r\n        <p><strong>Telefone:</strong> (21) 8765-4321</p>\r\n        <p><strong>Email:</strong> joao.silva@email.com</p>\r\n    </div>\r\n\r\n    <!-- Tabela de Itens -->\r\n    <div class=\"section\">\r\n        <h2>Itens</h2>\r\n        <table>\r\n            <thead>\r\n                <tr>\r\n                    <th>Descrição</th>\r\n                    <th>Quantidade</th>\r\n                    <th>Preço Unitário (R$)</th>\r\n                    <th>Total (R$)</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n                <tr>\r\n                    <td>Produto A</td>\r\n                    <td>2</td>\r\n                    <td>50.00</td>\r\n                    <td>100.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Produto B</td>\r\n                    <td>1</td>\r\n                    <td>150.00</td>\r\n                    <td>150.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Serviço X</td>\r\n                    <td>3</td>\r\n                    <td>200.00</td>\r\n                    <td>600.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Serviço Y</td>\r\n                    <td>2</td>\r\n                    <td>300.00</td>\r\n                    <td>600.00</td>\r\n                </tr>\r\n            </tbody>\r\n            <tfoot>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Subtotal</td>\r\n                    <td class=\"total\">1450.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Impostos (10%)</td>\r\n                    <td class=\"total\">145.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Total</td>\r\n                    <td class=\"total\">1595.00</td>\r\n                </tr>\r\n            </tfoot>\r\n        </table>\r\n    </div>\r\n\r\n    <!-- Rodapé da Fatura -->\r\n    <div class=\"footer\">\r\n        <p>Obrigado por fazer negócios conosco!</p>\r\n        <p>Se tiver alguma dúvida sobre esta fatura, entre em contato.</p>\r\n    </div>\r\n</div>\r\n\r\n</body>\r\n</html>\r\n";
}