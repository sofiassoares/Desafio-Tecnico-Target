using DesafioTecnico.App.Services;
using Xunit;

namespace DesafioTecnico.Tests
{
    public class ComissaoTests
    {
        [Theory]
        [InlineData(50, 0)]
        [InlineData(300, 3)]  
        [InlineData(500, 25)] 
        [InlineData(2000, 100)] 
        public void Deve_Calcular_Comissao_certo(decimal valorVenda, decimal esperado)
        {
            var service = new ComissaoService();

            var resultado = service.Calcular(valorVenda);

            Assert.Equal(esperado, resultado);
        }
    }
}
