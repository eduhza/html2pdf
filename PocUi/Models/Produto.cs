namespace PocUi.Models;

public sealed class Produto
{
    public long Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public decimal Preco { get; set; }

    public decimal Quantidade { get; set; }
}
