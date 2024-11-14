using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace PocAPI.iTextSharpLib;

public class ItextSharpConverter : IItextSharpConverter
{
    public Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken = default)
    {
        StringReader sr = new StringReader(htmlContent);
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);

        using MemoryStream memoryStream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
        pdfDoc.Open();
        cancellationToken.ThrowIfCancellationRequested();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        return Task.FromResult(memoryStream.ToArray());
    }
}
