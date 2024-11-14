using PocAPI.SyncfusionLib;
using Syncfusion.Licensing;

namespace PocAPI.Extensions;

public static class SyncfusionExtension
{
    public static IServiceCollection AddSyncfusion(this IServiceCollection services, ConfigurationManager configuration)
    {
        SyncfusionLicenseProvider.RegisterLicense(configuration["Syncfusion:LicenseKey"]);
        services.AddScoped<SyncfusionUseCase>();
        services.AddScoped<ISyncfusionConverter, SyncfusionConverter>();

        return services;
    }
}
