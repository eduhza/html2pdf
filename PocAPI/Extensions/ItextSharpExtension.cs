using PocAPI.iTextSharpLib;

namespace PocAPI.Extensions;

public static class ItextSharpExtension
{
    public static IServiceCollection AddITextSharp(this IServiceCollection services)
    {
        services.AddScoped<ItextSharpUseCase>();
        services.AddScoped<IItextSharpConverter, ItextSharpConverter>();

        return services;
    }
}
