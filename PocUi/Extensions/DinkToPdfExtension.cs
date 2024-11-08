using DinkToPdf;
using DinkToPdf.Contracts;
using PocUi.DinkToPdf;

namespace PocUi.Extensions;

public static class DinkToPdfExtension
{
    public static IServiceCollection AddDinkToPdf(this IServiceCollection services)
    {
        services.AddScoped<DinkToPdfUseCase>();
        services.AddSingleton<IConverter, SynchronizedConverter>(provider =>
            new SynchronizedConverter(new PdfTools()));
        services.AddScoped<IIDinkToPdfConverter, DinkToPdfConverter>();

        return services;
    }
}
