using PocAPI.GotenbergLib;

namespace PocAPI.Extensions;

//Gotenberg -> A Container API for converting HTML, Markdown, MS Office, and more to PDF
public static class GotenbergExtension
{
    public static IServiceCollection AddGotenberg(this IServiceCollection services, ConfigurationManager configuration)
    {
        var gotenbergBaseUrl = configuration.GetSection("Gotenberg")["BaseUrl"];
        if (string.IsNullOrEmpty(gotenbergBaseUrl))
        {
            throw new InvalidOperationException("Gotenberg BaseUrl is not configured.");
        }
        services.AddScoped<GotenbergUseCase>();
        services.AddHttpClient<IGoternbergConverter, GoternbergConverter>(client =>
            client.BaseAddress = new Uri(gotenbergBaseUrl));

        return services;
    }
}
