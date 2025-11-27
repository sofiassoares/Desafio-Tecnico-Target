using DesafioTecnico.App.Core;
using DesafioTecnico.App.Repositories;
using DesafioTecnico.App.Services;

string caminhoVendas = @"C:\Users\sofia\Documents\Desafiotecnico\src\DesafioTecnico.App\Data\vendas.json";
string caminhoEstoque = @"C:\Users\sofia\Documents\Desafiotecnico\src\DesafioTecnico.App\Data\estoque.json";

var vendasRepo = new VendasRepository(caminhoVendas);
var estoqueRepo = new EstoqueRepository(caminhoEstoque);

var listaProdutos = estoqueRepo.ObterProdutos();

var comissaoService = new ComissaoService();
var estoqueService = new EstoqueService(listaProdutos);
var jurosService = new JurosService();

var menu = new Menu(vendasRepo, estoqueRepo, comissaoService, estoqueService, jurosService);

menu.Exibir();
