using DesafioTecnico.App.Models;

namespace DesafioTecnico.App.Services
{
    public class EstoqueService
    {
        private readonly List<Produto> _produtos;
        public EstoqueService(List<Produto> produtos)
        {
            _produtos = produtos;
        }
        public int Movimentar(Movimentacao mov)
        {
            var produto = _produtos.FirstOrDefault(p => p.CodigoProduto == mov.CodigoProduto);
            if (produto == null)
                throw new Exception("produto não encontrado");
            produto.Estoque += mov.Quantidade;

            return produto.Estoque;
        }
    }
}
