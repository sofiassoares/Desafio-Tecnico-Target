using DesafioTecnico.App.Repositories;
using DesafioTecnico.App.Services;

namespace DesafioTecnico.App.Core
{
    public class Menu
    {
        private readonly VendasRepository _vendasRepository;
        private readonly EstoqueRepository _estoqueRepository;
        private readonly ComissaoService _comissaoService;
        private readonly EstoqueService _estoqueService;
        private readonly JurosService _jurosService;

        public Menu(
            VendasRepository vendasRepository,
            EstoqueRepository estoqueRepository,
            ComissaoService comissaoService,
            EstoqueService estoqueService,
            JurosService jurosService)
        {
            _vendasRepository = vendasRepository;
            _estoqueRepository = estoqueRepository;
            _comissaoService = comissaoService;
            _estoqueService = estoqueService;
            _jurosService = jurosService;
        }

        public void Exibir()
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.Clear();
                Console.WriteLine("Menu");
                Console.WriteLine("1 - Calcular comissão");
                Console.WriteLine("2 - Movimentar estoque");
                Console.WriteLine("3 - Calcular juros");
                Console.WriteLine("0 - Sair");
                Console.Write("> ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        CalcularComissoes();
                        break;
                    case 2:
                        MovimentarEstoque();
                        break;
                    case 3:
                        CalcularJuros();
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione ENTER para continuar...");
                    Console.ReadLine();
                }
            }
        }
        private void CalcularComissoes()
        {
            var vendas = _vendasRepository.ObterVendas();

            Console.WriteLine("Comissão dos vendedores");

            var grupos = vendas.GroupBy(v => v.Vendedor);

            foreach (var grupo in grupos)
            {
                decimal totalComissao = grupo.Sum(v => _comissaoService.Calcular(v.Valor));
                Console.WriteLine($"{grupo.Key}: R$ {totalComissao:F2}");
            }
        }

        private void MovimentarEstoque()
        {
            Console.WriteLine("Movimentacao do estoque");
            Console.Write("Código do produto: ");
            int codigo = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Descrição da movimentação: ");
            string? descricao = Console.ReadLine();

            Console.Write("Quantidade (positiva = entrada, negativa = saída): ");
            int quantidade = int.Parse(Console.ReadLine() ?? "0");

            var mov = new DesafioTecnico.App.Models.Movimentacao
            {
                CodigoProduto = codigo,
                Descricao = descricao,
                Quantidade = quantidade
            };

            int estoqueFinal = _estoqueService.Movimentar(mov);

            Console.WriteLine($"\nEstoque final: {estoqueFinal}");

            _estoqueRepository.Salvar();
        }
        private void CalcularJuros()
        {
            Console.WriteLine("Calculo de juros");
            Console.Write("Valor original: ");
            decimal valor = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Data de vencimento (yyyy-MM-dd): ");
            DateTime data = DateTime.Parse(Console.ReadLine() ?? "");

            decimal juros = _jurosService.Calcular(valor, data);

            Console.WriteLine($"\nJuros acumulado: R$ {juros:F2}");
            Console.WriteLine($"Valor total: R$ {(valor + juros):F2}");
        }
    }
}
