using PocAPI.Services;

namespace PocAPI.Extensions;

public static class InvoiceExtension
{
    public static IServiceCollection AddInvoiceFactory(this IServiceCollection services)
    {
        services.AddRazorTemplating();
        services.AddScoped<InvoiceFactory>();

        return services;
    }
}
