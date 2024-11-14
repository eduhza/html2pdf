namespace PocAPI.Models;

public sealed class Invoice
{
    public string Number { get; set; } = string.Empty;

    public DateOnly IssuedDate { get; set; }

    public DateOnly DueDate { get; set; }

    public Endereco? EnderecoVendedor { get; set; }

    public Endereco? EnderecoCliente { get; set; }

    public Produto[] Produtos { get; set; } = [];
}
