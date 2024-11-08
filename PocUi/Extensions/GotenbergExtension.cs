using PocUi.Gotenberg;
using PocUi.Services;

namespace PocUi.Extensions;

//Gotenberg -> A Container API for converting HTML, Markdown, MS Office, and more to PDF
public static class GotenbergExtension
{
    public static IServiceCollection AddGotenberg(this IServiceCollection services, ConfigurationManager configuration)
    {
        var gotenbergBaseUrl = configuration.GetSection("Gotenberg")["BaseUrl"];
        services.AddScoped<GotenbergUseCase>();
        services.AddHttpClient<IGoternbergConverter, GoternbergConverter>(client =>
            client.BaseAddress = new Uri(gotenbergBaseUrl));

        return services;
    }
}
