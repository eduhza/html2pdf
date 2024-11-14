using PocAPI.PugPdfLib;

namespace PocAPI.Extensions;

public static class PugPdfExtension
{
    public static IServiceCollection AddPugPdf(this IServiceCollection services)
    {
        services.AddScoped<PugPdfUseCase>();
        services.AddSingleton<IPugPdfConverter, PugPdfConverter>();

        return services;
    }
}
