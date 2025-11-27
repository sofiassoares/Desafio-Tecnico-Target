using System.Text.Json.Serialization;

namespace DesafioTecnico.App.Models
{
    public class Produto
    {
        [JsonPropertyName("codigoProduto")]
        public int CodigoProduto { get; set; }
        [JsonPropertyName("descricaoProduto")]
        public string DescricaoProduto { get; set; } = string.Empty;

        [JsonPropertyName("estoque")]
        public int Estoque { get; set; }
    }
}
