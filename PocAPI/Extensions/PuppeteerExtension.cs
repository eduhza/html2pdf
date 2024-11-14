using PocAPI.PuppeteerLib;
using PuppeteerSharp;

namespace PocAPI.Extensions;

public static class PuppeteerExtension
{
    public static IServiceCollection AddPuppeteer(this IServiceCollection services)
    {
        var browserFetcher = new BrowserFetcher();
        browserFetcher.DownloadAsync().GetAwaiter().GetResult();

        services.AddScoped<PuppeteerUseCase>();
        services.AddSingleton<IPuppeteerConverter, PuppeteerConverter>();

        return services;
    }
}
