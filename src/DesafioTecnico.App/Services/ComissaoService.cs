namespace DesafioTecnico.App.Services
{
    public class ComissaoService
    {
        public decimal Calcular(decimal valor)
        {
            if (valor <100)
                return 0m;
            if (valor <500)
                return valor * 0.01m;
            return valor * 0.05m;
        }
    }
}