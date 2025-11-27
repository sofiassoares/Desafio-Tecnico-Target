using DesafioTecnico.App.Models;
using DesafioTecnico.App.Services;
using Xunit;

namespace DesafioTecnico.Tests
{
    public class EstoqueTests
    {
        [Fact]
        public void Deve_Adicionar_Estoque()
        {
            var produtos = new List<Produto>
            {
                new Produto { CodigoProduto = 10, DescricaoProduto = "Teste", Estoque = 20 }
            };

            var service = new EstoqueService(produtos);
            var mov = new Movimentacao
            {
                CodigoProduto = 10,
                Descricao = "entrada",
                Quantidade = 5
            };

            var estoqueFinal = service.Movimentar(mov);

            Assert.Equal(25, estoqueFinal);
        }
        [Fact]
        public void Deve_Retirar_Estoque()
        {
            var produtos = new List<Produto>
            {
                new Produto { CodigoProduto = 10, DescricaoProduto = "Teste", Estoque = 20 }
            };

            var service = new EstoqueService(produtos);
            var mov = new Movimentacao
            {
                CodigoProduto = 10,
                Descricao = "saida",
                Quantidade = -3
            };

            var estoqueFinal = service.Movimentar(mov);
            Assert.Equal(17, estoqueFinal);
        }
    }
}
