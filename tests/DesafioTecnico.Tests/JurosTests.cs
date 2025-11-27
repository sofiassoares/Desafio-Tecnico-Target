using DesafioTecnico.App.Services;
using Xunit;

namespace DesafioTecnico.Tests
{
    public class JurosTests
    {
        [Fact]
        public void Retornar_Zero_Se_Nao_Estiver_Em_Atraso()
        {
            var service = new JurosService();

            var juros = service.Calcular(1000, DateTime.Today);

            Assert.Equal(0, juros);
        }

        [Fact]
        public void Deve_Calcular_Juros_Corretamente()
        {
            var service = new JurosService();
            var vencimento = DateTime.Today.AddDays(-4);

            var jurosEsperado = 1000 * 0.025m * 4;

            var juros = service.Calcular(1000, vencimento);

            Assert.Equal(jurosEsperado, juros);
        }
    }
}
