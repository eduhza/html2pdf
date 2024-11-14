using Bogus;
using PocAPI.Models;
using Razor.Templating.Core;

namespace PocAPI.Services;

internal sealed class InvoiceFactory
{
    private readonly Invoice _invoice;
    public readonly string Html;
    public InvoiceFactory(IRazorTemplateEngine _razorTemplateEngine)
    {
        _invoice = Create();
        //Html = _razorTemplateEngine.RenderAsync("Views/PaginaModelo.cshtml", _invoice).Result;

        //Get a simple html
        Html = GetHtml.simpleHtml;
    }

    public Invoice Create()
    {
        var faker = new Faker();

        return new Invoice
        {
            Number = faker.Random.Number(100_000, 1_000_000).ToString(),
            IssuedDate = DateOnly.FromDateTime(DateTime.UtcNow),
            DueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
            EnderecoVendedor = new Endereco
            {
                NomeFantasia = faker.Company.CompanyName(),
                Rua = faker.Address.StreetAddress(),
                Cidade = faker.Address.City(),
                Estado = faker.Address.State(),
                Email = faker.Internet.Email()
            },
            EnderecoCliente = new Endereco
            {
                NomeFantasia = faker.Company.CompanyName(),
                Rua = faker.Address.StreetAddress(),
                Cidade = faker.Address.City(),
                Estado = faker.Address.State(),
                Email = faker.Internet.Email()
            },
            Produtos = Enumerable
                .Range(1, 13)
                .Select(i => new Produto
                {
                    Id = i,
                    Nome = faker.Commerce.ProductName(),
                    Preco = faker.Random.Decimal(10, 1000),
                    Quantidade = faker.Random.Decimal(1, 10)
                })
                .ToArray()
        };
    }
}
