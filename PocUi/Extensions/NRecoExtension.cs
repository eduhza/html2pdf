using PocUi.NRecoLib;

namespace PocUi.Extensions;

//NReco.PdfGenerator https://www.nrecosite.com/pdf_generator_net.aspx
public static class NRecoExtension
{
    public static IServiceCollection AddNReco(this IServiceCollection services)
    {
        services.AddScoped<NRecoUseCase>();
        services.AddScoped<INRecoConverter, NRecoConverter>();

        return services;
    }
}
