using NReco.PdfGenerator;

namespace PocUi.NRecoLib;

public class NRecoConverter() : INRecoConverter
{
    public Task<byte[]> GerarPdf(string htmlContent)
    {
        Console.WriteLine("GERANDO PDF NRecoConverter");
        var htmlToPdf = new HtmlToPdfConverter();
        var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);
        //var pdfBytes = htmlToPdf.GeneratePdfFromFile("https://ironpdf.com/blog/compare-to-other-components/nreco-net-core-html-to-pdf-alternatives/", null);
        return Task.FromResult(pdfBytes);
    }

    //public Task<byte[]> GerarPdf(string htmlContent)
    //{
    //    string DemoLicenseKey = "pjfsL9eBhU5mER7ULRNO/pgqeXBsJF15ea8d+vpzJ/ja8LpELgrs2FoaZwNLRmsJpgwKahfzSvyCZDHZsI0Bs9KCaby6CXo02YxpA3iiwJPaDdfO+vTK/JCsjH/d9l6118KUVjegNFAraGSKA3Q6tMTg4/injiZ9wZ3kcjoXPjs=";

    //    var htmlToPdf = new HtmlToPdfConverter();
    //    htmlToPdf.License.SetLicenseKey(
    //        "DEMO", DemoLicenseKey);

    //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    //    {
    //        htmlToPdf.WkHtmlToPdfExeName = "wkhtmltopdf.exe";
    //        htmlToPdf.PdfToolPath = "<path_to_folder_with_wkhtmltopdf>";
    //    }
    //    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) // for Linux/OS-X: "wkhtmltopdf"
    //    {
    //        htmlToPdf.WkHtmlToPdfExeName = "wkhtmltopdf";
    //        htmlToPdf.PdfToolPath = "<path_to_folder_with_wkhtmltopdf>";
    //    }
    //    else
    //    {
    //        throw new ApplicationException("Plataforma não suportada");
    //    }

    //    var pdfBytes = htmlToPdf.GeneratePdf(htmlContent);

    //    return Task.FromResult(pdfBytes);
    //}
}
