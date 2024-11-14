using Syncfusion.HtmlConverter;

namespace PocAPI.SyncfusionLib;

//https://help.syncfusion.com/document-processing/pdf/conversions/html-to-pdf/net/features#html-string-to-pdf
//https://github.com/SyncfusionExamples/PDF-Examples/blob/master/HTML%20to%20PDF/Blink/Convert-the-HTML-string-to-PDF-document/.NET/Convert-the-HTML-string-to-PDF-document/Program.cs
public class SyncfusionConverter : ISyncfusionConverter
{
    public Task<byte[]> GerarPdf(string htmlContent, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("GERANDO PDF SyncfusionConverter");

        //Initialize HTML to PDF converter.
        var htmlConverter = new HtmlToPdfConverter();
        BlinkConverterSettings blinkConverterSettings = new();
        blinkConverterSettings.CommandLineArguments.Add("--no-sandbox");
        blinkConverterSettings.CommandLineArguments.Add("--disable-setuid-sandbox");
        blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(1280, 0);
        blinkConverterSettings.TempPath = @"C:\Users\ea23318\Desktop"; //C:\Users\ea23318\Desktop //Path.GetTempPath()
        htmlConverter.ConverterSettings = blinkConverterSettings;
        Syncfusion.Pdf.PdfDocument document = htmlConverter.Convert("https://www.syncfusion.com");
        //string baseUrl = string.Empty; //To set the base URL for the images in the HTML content.
        //htmlContent = "<!DOCTYPE html>\r\n<html lang=\"pt-BR\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Fatura</title>\r\n    <style>\r\n        body { font-family: Arial, sans-serif; margin: 0; padding: 0; }\r\n        .container { width: 80%; margin: 20px auto; border: 1px solid #ddd; padding: 20px; }\r\n        .header, .footer { text-align: center; margin-bottom: 20px; }\r\n        .header h1 { margin: 0; }\r\n        .section { margin-bottom: 20px; }\r\n        .section h2 { margin: 0 0 10px 0; font-size: 1.2em; }\r\n        table { width: 100%; border-collapse: collapse; margin-top: 10px; }\r\n        th, td { padding: 10px; border: 1px solid #ddd; text-align: left; }\r\n        th { background-color: #f4f4f4; }\r\n        .total { font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<div class=\"container\">\r\n    <!-- Header da Fatura -->\r\n    <div class=\"header\">\r\n        <h1>Fatura</h1>\r\n        <p><strong>Número da Fatura:</strong> 123456</p>\r\n        <p><strong>Data de Emissão:</strong> 2024-11-01</p>\r\n        <p><strong>Data de Vencimento:</strong> 2024-11-15</p>\r\n    </div>\r\n\r\n    <!-- Informações do Fornecedor -->\r\n    <div class=\"section\">\r\n        <h2>Fornecedor</h2>\r\n        <p><strong>Nome:</strong> Empresa Exemplo Ltda.</p>\r\n        <p><strong>Endereço:</strong> Rua Exemplo, 123 - São Paulo, SP</p>\r\n        <p><strong>Telefone:</strong> (11) 1234-5678</p>\r\n        <p><strong>Email:</strong> contato@empresaexemplo.com</p>\r\n    </div>\r\n\r\n    <!-- Informações do Cliente -->\r\n    <div class=\"section\">\r\n        <h2>Cliente</h2>\r\n        <p><strong>Nome:</strong> João Silva</p>\r\n        <p><strong>Endereço:</strong> Avenida Central, 456 - Rio de Janeiro, RJ</p>\r\n        <p><strong>Telefone:</strong> (21) 8765-4321</p>\r\n        <p><strong>Email:</strong> joao.silva@email.com</p>\r\n    </div>\r\n\r\n    <!-- Tabela de Itens -->\r\n    <div class=\"section\">\r\n        <h2>Itens</h2>\r\n        <table>\r\n            <thead>\r\n                <tr>\r\n                    <th>Descrição</th>\r\n                    <th>Quantidade</th>\r\n                    <th>Preço Unitário (R$)</th>\r\n                    <th>Total (R$)</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n                <tr>\r\n                    <td>Produto A</td>\r\n                    <td>2</td>\r\n                    <td>50.00</td>\r\n                    <td>100.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Produto B</td>\r\n                    <td>1</td>\r\n                    <td>150.00</td>\r\n                    <td>150.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Serviço X</td>\r\n                    <td>3</td>\r\n                    <td>200.00</td>\r\n                    <td>600.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Serviço Y</td>\r\n                    <td>2</td>\r\n                    <td>300.00</td>\r\n                    <td>600.00</td>\r\n                </tr>\r\n            </tbody>\r\n            <tfoot>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Subtotal</td>\r\n                    <td class=\"total\">1450.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Impostos (10%)</td>\r\n                    <td class=\"total\">145.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Total</td>\r\n                    <td class=\"total\">1595.00</td>\r\n                </tr>\r\n            </tfoot>\r\n        </table>\r\n    </div>\r\n\r\n    <!-- Rodapé da Fatura -->\r\n    <div class=\"footer\">\r\n        <p>Obrigado por fazer negócios conosco!</p>\r\n        <p>Se tiver alguma dúvida sobre esta fatura, entre em contato.</p>\r\n    </div>\r\n</div>\r\n\r\n</body>\r\n</html>\r\n";
        //htmlContent = "<html><body><p> Hello World</p></body></html>";
        //Syncfusion.Pdf.PdfDocument document = htmlConverter.Convert(htmlContent, baseUrl);

        cancellationToken.ThrowIfCancellationRequested();
        using MemoryStream memoryStream = new();
        document.Save(memoryStream);
        var bytePdf = memoryStream.ToArray();
        return Task.FromResult(bytePdf);
    }
}


