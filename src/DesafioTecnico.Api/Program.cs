using DesafioTecnico.App.Models;
using DesafioTecnico.App.Repositories;
using DesafioTecnico.App.Services;

var builder = WebApplication.CreateBuilder(args);

// mude aqui os caminhos
string caminhoVendas  = @"C:sofia\Documents\Desafiotecnico\src\DesafioTecnico.App\Data\vendas.json";
string caminhoEstoque = @"C:sofia\Documents\Desafiotecnico\src\DesafioTecnico.App\Data\estoque.json";

builder.Services.AddSingleton(new VendasRepository(caminhoVendas));
builder.Services.AddSingleton(new EstoqueRepository(caminhoEstoque));

builder.Services.AddSingleton<ComissaoService>();
builder.Services.AddSingleton<JurosService>();

builder.Services.AddSingleton<EstoqueService>(sp =>
{
    var repo = sp.GetRequiredService<EstoqueRepository>();
    return new EstoqueService(repo.ObterProdutos());
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();

app.MapGet("/api/estoque", (EstoqueRepository repo) =>
{
    return Results.Ok(repo.ObterProdutos());
});

app.MapGet("/api/estoque/{codigo:int}", (int codigo, EstoqueRepository repo) =>
{
    var produto = repo.ObterProdutos().FirstOrDefault(p => p.CodigoProduto == codigo);

    return produto is null
        ? Results.NotFound(new { mensagem = "Produto não encontrado" })
        : Results.Ok(produto);
});

app.MapPost("/api/estoque/movimentar", (Movimentacao mov, EstoqueService service, EstoqueRepository repo) =>
{
    var estoqueFinal = service.Movimentar(mov);
    repo.Salvar();

    return Results.Ok(new
    {
        mov.CodigoProduto,
        mov.Quantidade,
        estoqueFinal
    });
});

app.MapGet("/api/vendas", (VendasRepository repo) =>
{
    return Results.Ok(repo.ObterVendas());
});

app.MapGet("/api/comissoes", (VendasRepository repo, ComissaoService comissaoService) =>
{
    var vendas = repo.ObterVendas();

    var resultado = vendas
        .GroupBy(v => v.Vendedor)
        .Select(g => new
        {
            Vendedor = g.Key,
            ComissaoTotal = g.Sum(v => comissaoService.Calcular(v.Valor))
        });

    return Results.Ok(resultado);
});

app.MapGet("/api/juros", (decimal valor, string data, JurosService jurosService) =>
{
    if (!DateTime.TryParseExact(data, "dd-MM-yyyy", null,
        System.Globalization.DateTimeStyles.None, out var dt))
    {
        return Results.BadRequest(new { mensagem = "Data inválida. Use dd-MM-yyyy." });
    }

    var juros = jurosService.Calcular(valor, dt);

    return Results.Ok(new
    {
        ValorOriginal = valor,
        DataVencimento = dt.ToString("dd/MM/yyyy"),
        Juros = juros,
        ValorTotal = valor + juros
    });
});

app.Run();
