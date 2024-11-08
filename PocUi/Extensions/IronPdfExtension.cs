using PocUi.IronPdf;

namespace PocUi.Extensions;

public static class IronPdfExtension
{
    public static IServiceCollection AddIronPdf(this IServiceCollection services, ConfigurationManager configuration)
    {
        License.LicenseKey = configuration["IronPdf:LicenseKey"];
        services.AddScoped<IronPdfUseCase>();
        services.AddScoped<IIronPdfConverter, IronPdfConverter>();

        return services;
    }
}
