using PocAPI.DinkToPdf;
using PocAPI.Extensions;
using PocAPI.GotenbergLib;
using PocAPI.Html2Pdf;
using PocAPI.IronPdf;
using PocAPI.iTextSharpLib;
using PocAPI.NRecoLib;
using PocAPI.PugPdfLib;
using PocAPI.SyncfusionLib;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
        //.WithOrigins("http://localhost:5001", "http://localhost:3000", "http://gotenberg:3000")
        .AllowAnyOrigin()
        .AllowAnyHeader()
        //.AllowCredentials()
        .AllowAnyMethod();
    });
});

builder.Services.AddInvoiceFactory();
builder.Services.AddDinkToPdf();
builder.Services.AddPugPdf();
//builder.Services.AddPuppeteer();
builder.Services.AddGotenberg(builder.Configuration);
builder.Services.AddIronPdf(builder.Configuration);
builder.Services.AddNReco();
builder.Services.AddSyncfusion(builder.Configuration);
builder.Services.AddITextSharp();
builder.Services.AddHtml2Pdf();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine("IsDevelopment");

    app.Use(async (context, next) =>
    {
        if (context.Request.Path.StartsWithSegments("/_framework"))
        {
            context.Response.StatusCode = 404;
            return;
        }
        await next();
    });

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    Console.WriteLine("Producao");
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.MapDinkToPdf();
app.MapPugPdf();
//app.MapPuppeteer();
app.MapGotenberg();
app.MapIronPdf();
app.MapNReco();
app.MapSyncfusion();
app.MapItextSharp();
app.MapHtml2Pdf();

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    Console.WriteLine("WINDOWS");
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    Console.WriteLine("LINUX");
}

app.Run();

internal static class GetHtml
{
    public static string htmlCss = "<script src=\"https://cdn.tailwindcss.com\"></script>\r\n\r\n<div class=\"min-w-7xl flex flex-col bg-gray-200 space-y-4 p-10\">\r\n    <h1 class=\"text-2xl font-semibold\">Invoice #869022</h1>\r\n\r\n    <p>Issued date: 06/11/2024</p>\r\n    <p>Due date: 16/11/2024</p>\r\n\r\n    <div class=\"flex justify-between space-x-4\">\r\n        <div class=\"bg-gray-100 rounded-lg flex flex-col space-y-1 p-4 w-1/2\">\r\n            <p class=\"font-medium\">Seller:</p>\r\n            <p>Skiles, Reichel and Jones</p>\r\n            <p>820 Goyette Oval</p>\r\n            <p>New Tyrel</p>\r\n            <p>Oklahoma</p>\r\n            <p>Miller97@gmail.com</p>\r\n        </div>\r\n        <div class=\"bg-gray-100 rounded-lg flex flex-col space-y-1 p-4 w-1/2\">\r\n            <p class=\"font-medium\">Bill to:</p>\r\n            <p>Rolfson Inc</p>\r\n            <p>5491 Stark Village</p>\r\n            <p>Nicolasland</p>\r\n            <p>Tennessee</p>\r\n            <p>Bradly_Veum@yahoo.com</p>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"flex flex-col bg-white rounded-lg p-4 space-y-2\">\r\n        <h2 class=\"text-xl font-medium\">Items:</h2>\r\n        <div class=\"\">\r\n            <div class=\"flex space-x-4 font-medium\">\r\n                <p class=\"w-10\">#</p>\r\n                <p class=\"w-52\">Name</p>\r\n                <p class=\"w-20\">Price</p>\r\n                <p class=\"w-20\">Quantity</p>\r\n            </div>\r\n\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">1</p>\r\n                    <p class=\"w-52\">Gorgeous Plastic Tuna</p>\r\n                    <p class=\"w-20\">R$ 586,35</p>\r\n                    <p class=\"w-20\">4,98</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">2</p>\r\n                    <p class=\"w-52\">Unbranded Rubber Hat</p>\r\n                    <p class=\"w-20\">R$ 521,84</p>\r\n                    <p class=\"w-20\">5,38</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">3</p>\r\n                    <p class=\"w-52\">Awesome Fresh Keyboard</p>\r\n                    <p class=\"w-20\">R$ 618,82</p>\r\n                    <p class=\"w-20\">5,81</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">4</p>\r\n                    <p class=\"w-52\">Gorgeous Frozen Mouse</p>\r\n                    <p class=\"w-20\">R$ 359,55</p>\r\n                    <p class=\"w-20\">1,88</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">5</p>\r\n                    <p class=\"w-52\">Incredible Rubber Cheese</p>\r\n                    <p class=\"w-20\">R$ 834,36</p>\r\n                    <p class=\"w-20\">6,41</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">6</p>\r\n                    <p class=\"w-52\">Practical Soft Fish</p>\r\n                    <p class=\"w-20\">R$ 432,86</p>\r\n                    <p class=\"w-20\">2,33</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">7</p>\r\n                    <p class=\"w-52\">Sleek Frozen Fish</p>\r\n                    <p class=\"w-20\">R$ 420,50</p>\r\n                    <p class=\"w-20\">1,61</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">8</p>\r\n                    <p class=\"w-52\">Ergonomic Granite Pants</p>\r\n                    <p class=\"w-20\">R$ 956,77</p>\r\n                    <p class=\"w-20\">3,91</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">9</p>\r\n                    <p class=\"w-52\">Unbranded Metal Mouse</p>\r\n                    <p class=\"w-20\">R$ 781,81</p>\r\n                    <p class=\"w-20\">4,66</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">10</p>\r\n                    <p class=\"w-52\">Refined Frozen Towels</p>\r\n                    <p class=\"w-20\">R$ 385,39</p>\r\n                    <p class=\"w-20\">4,10</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">11</p>\r\n                    <p class=\"w-52\">Unbranded Frozen Ball</p>\r\n                    <p class=\"w-20\">R$ 602,00</p>\r\n                    <p class=\"w-20\">9,84</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">12</p>\r\n                    <p class=\"w-52\">Awesome Concrete Bacon</p>\r\n                    <p class=\"w-20\">R$ 816,36</p>\r\n                    <p class=\"w-20\">7,64</p>\r\n                </div>\r\n                <div class=\"flex space-x-4\">\r\n                    <p class=\"w-10\">13</p>\r\n                    <p class=\"w-52\">Tasty Cotton Car</p>\r\n                    <p class=\"w-20\">R$ 442,53</p>\r\n                    <p class=\"w-20\">1,69</p>\r\n                </div>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"flex flex-col items-end bg-gray-50 space-y-2 p-4 rounded-lg\">\r\n        <p>Subtotal: R$ 38.900,82</p>\r\n        <p>Total: <span class=\"font-semibold\">R$ 38.900,82</span></p>\r\n    </div>\r\n</div>";
    public static string simpleHtml = "<!DOCTYPE html>\r\n<html lang=\"pt-BR\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Fatura</title>\r\n    <style>\r\n        body { font-family: Arial, sans-serif; margin: 0; padding: 0; }\r\n        .container { width: 80%; margin: 20px auto; border: 1px solid #ddd; padding: 20px; }\r\n        .header, .footer { text-align: center; margin-bottom: 20px; }\r\n        .header h1 { margin: 0; }\r\n        .section { margin-bottom: 20px; }\r\n        .section h2 { margin: 0 0 10px 0; font-size: 1.2em; }\r\n        table { width: 100%; border-collapse: collapse; margin-top: 10px; }\r\n        th, td { padding: 10px; border: 1px solid #ddd; text-align: left; }\r\n        th { background-color: #f4f4f4; }\r\n        .total { font-weight: bold; }\r\n    </style>\r\n</head>\r\n<body>\r\n\r\n<div class=\"container\">\r\n    <!-- Header da Fatura -->\r\n    <div class=\"header\">\r\n        <h1>Fatura</h1>\r\n        <p><strong>Número da Fatura:</strong> 123456</p>\r\n        <p><strong>Data de Emissão:</strong> 2024-11-01</p>\r\n        <p><strong>Data de Vencimento:</strong> 2024-11-15</p>\r\n    </div>\r\n\r\n    <!-- Informações do Fornecedor -->\r\n    <div class=\"section\">\r\n        <h2>Fornecedor</h2>\r\n        <p><strong>Nome:</strong> Empresa Exemplo Ltda.</p>\r\n        <p><strong>Endereço:</strong> Rua Exemplo, 123 - São Paulo, SP</p>\r\n        <p><strong>Telefone:</strong> (11) 1234-5678</p>\r\n        <p><strong>Email:</strong> contato@empresaexemplo.com</p>\r\n    </div>\r\n\r\n    <!-- Informações do Cliente -->\r\n    <div class=\"section\">\r\n        <h2>Cliente</h2>\r\n        <p><strong>Nome:</strong> João Silva</p>\r\n        <p><strong>Endereço:</strong> Avenida Central, 456 - Rio de Janeiro, RJ</p>\r\n        <p><strong>Telefone:</strong> (21) 8765-4321</p>\r\n        <p><strong>Email:</strong> joao.silva@email.com</p>\r\n    </div>\r\n\r\n    <!-- Tabela de Itens -->\r\n    <div class=\"section\">\r\n        <h2>Itens</h2>\r\n        <table>\r\n            <thead>\r\n                <tr>\r\n                    <th>Descrição</th>\r\n                    <th>Quantidade</th>\r\n                    <th>Preço Unitário (R$)</th>\r\n                    <th>Total (R$)</th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n                <tr>\r\n                    <td>Produto A</td>\r\n                    <td>2</td>\r\n                    <td>50.00</td>\r\n                    <td>100.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Produto B</td>\r\n                    <td>1</td>\r\n                    <td>150.00</td>\r\n                    <td>150.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Serviço X</td>\r\n                    <td>3</td>\r\n                    <td>200.00</td>\r\n                    <td>600.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td>Serviço Y</td>\r\n                    <td>2</td>\r\n                    <td>300.00</td>\r\n                    <td>600.00</td>\r\n                </tr>\r\n            </tbody>\r\n            <tfoot>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Subtotal</td>\r\n                    <td class=\"total\">1450.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Impostos (10%)</td>\r\n                    <td class=\"total\">145.00</td>\r\n                </tr>\r\n                <tr>\r\n                    <td colspan=\"3\" class=\"total\">Total</td>\r\n                    <td class=\"total\">1595.00</td>\r\n                </tr>\r\n            </tfoot>\r\n        </table>\r\n    </div>\r\n\r\n    <!-- Rodapé da Fatura -->\r\n    <div class=\"footer\">\r\n        <p>Obrigado por fazer negócios conosco!</p>\r\n        <p>Se tiver alguma dúvida sobre esta fatura, entre em contato.</p>\r\n    </div>\r\n</div>\r\n\r\n</body>\r\n</html>\r\n";
}
