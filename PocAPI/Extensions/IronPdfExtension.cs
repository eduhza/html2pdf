using PocAPI.IronPdf;

namespace PocAPI.Extensions;

public static class IronPdfExtension
{
    public static IServiceCollection AddIronPdf(this IServiceCollection services, ConfigurationManager configuration)
    {
        License.LicenseKey = configuration["IronPdf:LicenseKey"];
        services.AddScoped<IronPdfUseCase>();
        services.AddSingleton<ChromePdfRenderer>();
        services.AddScoped<IIronPdfConverter, IronPdfConverter>(provider =>
        {
            var renderer = provider.GetRequiredService<ChromePdfRenderer>();
            return new IronPdfConverter(renderer);
        });

        return services;
    }
}
