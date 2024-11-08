using PocUi.Services;

namespace PocUi.Extensions;

public static class InvoiceExtension
{
    public static IServiceCollection AddInvoiceFactory(this IServiceCollection services)
    {
        services.AddRazorTemplating();
        services.AddScoped<InvoiceFactory>();

        return services;
    }
}
