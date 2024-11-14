using PocAPI.PuppeteerLib;

namespace PocAPI.Extensions;

public static class PuppeteerExtension
{
    public static IServiceCollection AddPuppeteer(this IServiceCollection services)
    {
        services.AddScoped<PuppeteerUseCase>();
        services.AddSingleton<IPuppeteerConverter, PuppeteerConverter>();

        return services;
    }
}
