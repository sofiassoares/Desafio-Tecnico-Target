using System.Text.Json;
using DesafioTecnico.App.Models;

namespace DesafioTecnico.App.Repositories
{
    public class EstoqueRepository
    {
        private readonly List<Produto> _produtos;
        private readonly string _caminhoArquivo;
        public EstoqueRepository(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;

            var json = File.ReadAllText(caminhoArquivo);

            var obj = JsonSerializer.Deserialize<Dictionary<string, List<Produto>>>(json);

            _produtos = obj?["estoque"] ?? new List<Produto>();
        }
        public List<Produto> ObterProdutos()
        {
            return _produtos;
        }
        public void Salvar()
        {
            var obj = new Dictionary<string, List<Produto>>
            {
                { "estoque", _produtos }
            };

            var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(_caminhoArquivo, json);
        }
    }
}
