using PocUi.PuppeteerLib;

namespace PocUi.Extensions;

public static class PuppeteerExtension
{
    public static IServiceCollection AddPuppeteer(this IServiceCollection services)
    {
        services.AddScoped<PuppeteerUseCase>();
        services.AddScoped<IPuppeteerConverter, PuppeteerConverter>();

        return services;
    }
}
