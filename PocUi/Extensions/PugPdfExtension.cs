using PocUi.PugPdfLib;

namespace PocUi.Extensions;

public static class PugPdfExtension
{
    public static IServiceCollection AddPugPdf(this IServiceCollection services)
    {
        services.AddScoped<PugPdfUseCase>();
        services.AddScoped<IPugPdfConverter, PugPdfConverter>();

        return services;
    }
}
