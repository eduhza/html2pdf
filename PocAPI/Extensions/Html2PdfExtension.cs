using PocAPI.Html2Pdf;

namespace PocAPI.Extensions;

public static class Html2PdfExtension
{
    public static IServiceCollection AddHtml2Pdf(this IServiceCollection services)
    {
        services.AddScoped<Html2PdfUseCase>();
        services.AddScoped<IHtml2PdfConverter, Html2PdfConverter>();

        return services;
    }
}
