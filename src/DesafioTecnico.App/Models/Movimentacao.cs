public class Movimentacao
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public int CodigoProduto { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }
}
