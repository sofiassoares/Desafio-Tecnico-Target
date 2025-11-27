using System.Text.Json;
using DesafioTecnico.App.Models;

namespace DesafioTecnico.App.Repositories
{
    public class VendasRepository
    {
        private readonly List<Venda> _vendas;

        public VendasRepository(string caminhoArquivo)
        {
            var json = File.ReadAllText(caminhoArquivo);
            var obj = JsonSerializer.Deserialize<Dictionary<string, List<Venda>>>(json);

            _vendas = obj?["vendas"] ?? new List<Venda>();
        }
        public IReadOnlyList<Venda> ObterVendas()
        {
            return _vendas;
        }
    }
}
