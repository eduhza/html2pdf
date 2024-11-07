using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using PocUi.IronPdf;

namespace BenchmarkTest;

public class PdfBenchmarks
{
    public readonly string _htmlContent;
    //private readonly HttpClient _httpClient;
    //private readonly GotenbergUseCase _gotenberg;
    private readonly IronPdfUseCase _ironPdf;

    public PdfBenchmarks()
    {
        _htmlContent = GetHtml.htmlContent;
        //_httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:3000") };
        _ironPdf = new IronPdfUseCase(new IronPdfConverter());
        //_gotenberg = new GotenbergUseCase(new GoternbergConverter(_httpClient));
    }

    [Benchmark]
    public async Task BenchmarkIronPdf() => await _ironPdf.ExecuteAsync(_htmlContent);

    //[Benchmark]
    //public async Task BenchmarkGotenberg() => await _gotenberg.ExecuteAsync(_htmlContent);

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
    public static string htmlContent = "<script src=\"https://cdn.tailwindcss.com\"></script>\r\n\r\n<div class=\"min-w-7xl flex flex-col bg-gray-200 space-y-4 p-10\">\r\n    <h1 class=\"text-2xl font-semibold\">Invoice #869022</h1>\r\n\r\n    <p>Issued date: 06/11/2024</p>\r\n    <p>Due date: 16/11/2024</p>\r\n\r\n    <div class=\"flex justify-between space-x-4\">\r\n        <div class=\"bg-gray-100 rounded-lg flex flex-col space-y-1 p-4 w-1/2\">\r\n            <p class=\"font-medium\">Seller:</p>\r\n            <p>Skiles, Reichel and Jones</p>\r\n            <p>820 Goyette Oval</p>\r\n            <p>New Tyrel</p>\r\n            <p>Oklahoma</p>\r\n            <p>Miller97@gmail.com</p>\r\n        </div>\r\n        <div class=\"bg-gray-100 rounded-lg flex flex-col space-y-1 p-4 w-1/2\">\r\n            <p class=\"font-medium\">Bill to:</p>\r\n            <p>Rolfson Inc</p>\r\n            <p>5491 Stark Village</p>\r\n            <p>Nicolasland</p>\r\n            <p>Tennessee</p>\r\n            <p>Bradly_Veum@yahoo.com</p>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"flex flex-col bg-white rounded-lg p-4 space-y-2\">\r\n        <h2 class=\"text-xl font-medium\">Items:</h2>\r\n        <div class=\"\">\r\n            <div class=\"flex space-x-4 font-medium\">\r\n                <p class=\"w-10\">#</p>\r\n                <p class=\"w-52\">Name</p>\r\n                <p class=\"w-20\">Price</p>\r\n                <p class=\"w-20\">Quantity</p>\r\n            </div>\r\n\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">1</p>\r\n                    <p class=\"w-52\">Gorgeous Plastic Tuna</p>\r\n                    <p class=\"w-20\">R$ 586,35</p>\r\n                    <p class=\"w-20\">4,98</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">2</p>\r\n                    <p class=\"w-52\">Unbranded Rubber Hat</p>\r\n                    <p class=\"w-20\">R$ 521,84</p>\r\n                    <p class=\"w-20\">5,38</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">3</p>\r\n                    <p class=\"w-52\">Awesome Fresh Keyboard</p>\r\n                    <p class=\"w-20\">R$ 618,82</p>\r\n                    <p class=\"w-20\">5,81</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">4</p>\r\n                    <p class=\"w-52\">Gorgeous Frozen Mouse</p>\r\n                    <p class=\"w-20\">R$ 359,55</p>\r\n                    <p class=\"w-20\">1,88</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">5</p>\r\n                    <p class=\"w-52\">Incredible Rubber Cheese</p>\r\n                    <p class=\"w-20\">R$ 834,36</p>\r\n                    <p class=\"w-20\">6,41</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">6</p>\r\n                    <p class=\"w-52\">Practical Soft Fish</p>\r\n                    <p class=\"w-20\">R$ 432,86</p>\r\n                    <p class=\"w-20\">2,33</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">7</p>\r\n                    <p class=\"w-52\">Sleek Frozen Fish</p>\r\n                    <p class=\"w-20\">R$ 420,50</p>\r\n                    <p class=\"w-20\">1,61</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">8</p>\r\n                    <p class=\"w-52\">Ergonomic Granite Pants</p>\r\n                    <p class=\"w-20\">R$ 956,77</p>\r\n                    <p class=\"w-20\">3,91</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">9</p>\r\n                    <p class=\"w-52\">Unbranded Metal Mouse</p>\r\n                    <p class=\"w-20\">R$ 781,81</p>\r\n                    <p class=\"w-20\">4,66</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">10</p>\r\n                    <p class=\"w-52\">Refined Frozen Towels</p>\r\n                    <p class=\"w-20\">R$ 385,39</p>\r\n                    <p class=\"w-20\">4,10</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">11</p>\r\n                    <p class=\"w-52\">Unbranded Frozen Ball</p>\r\n                    <p class=\"w-20\">R$ 602,00</p>\r\n                    <p class=\"w-20\">9,84</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">12</p>\r\n                    <p class=\"w-52\">Awesome Concrete Bacon</p>\r\n                    <p class=\"w-20\">R$ 816,36</p>\r\n                    <p class=\"w-20\">7,64</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">13</p>\r\n                    <p class=\"w-52\">Tasty Cotton Car</p>\r\n                    <p class=\"w-20\">R$ 442,53</p>\r\n                    <p class=\"w-20\">1,69</p>\r\n                </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"flex flex-col items-end bg-gray-50 space-y-2 p-4 rounded-lg\">\r\n        <p>Subtotal: R$ 38.900,82</p>\r\n        <p>Total: <span class=\"font-semibold\">R$ 38.900,82</span></p>\r\n    </div>\r\n</div>";
}