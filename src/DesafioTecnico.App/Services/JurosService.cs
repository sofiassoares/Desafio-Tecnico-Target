namespace DesafioTecnico.App.Services
{
    public class JurosService
    {
        public decimal Calcular(decimal valorOriginal, DateTime vencimento)
        {
            int diasAtraso = (DateTime.Today - vencimento).Days;
            if (diasAtraso <= 0)
                return 0m;
            decimal juros = valorOriginal * 0.025m * diasAtraso;

            return juros;
        }
    }
}
