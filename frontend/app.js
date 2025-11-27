const API_BASE = "http://localhost:5259";

function mostrarAba(id) {
    const abas = document.querySelectorAll(".aba");
    const botoes = document.querySelectorAll(".tabButton");

    abas.forEach(aba => {
        aba.classList.remove("ativa");
    });

    botoes.forEach(btn => {
        btn.classList.remove("ativo");
    });

    const selecionada = document.getElementById(id);
    if (selecionada) {
        selecionada.classList.add("ativa");
    }
    const btn = Array.from(botoes).find(b => b.textContent.toLowerCase().includes(id));
    if (btn) {
        btn.classList.add("ativo");
    }
}

function carregarEstoque() {
    fetch(`${API_BASE}/api/estoque`)
        .then(r => {
            if (!r.ok) throw new Error("Erro HTTP");
            return r.json();
        })
        .then(produtos => {
            const tabela = document.getElementById("tabelaEstoque");
            const totalItens = document.getElementById("totalItens");

            tabela.innerHTML = "";
            let soma = 0;

            produtos.forEach(p => {
                soma += p.estoque;

                const tr = document.createElement("tr");
                tr.innerHTML = `
                    <td>${p.codigoProduto}</td>
                    <td>${p.descricaoProduto}</td>
                    <td>${p.estoque}</td>
                `;
                tabela.appendChild(tr);
            });

            totalItens.textContent = `Total de itens no estoque: ${soma}`;
        })
        .catch(err => {
            console.error("Erro ao carregar estoque:", err);
            document.getElementById("totalItens").textContent = "Erro ao carregar dados da API.";
        });
}

function carregarVendas() {
    fetch(`${API_BASE}/api/vendas`)
        .then(r => {
            if (!r.ok) throw new Error("Erro HTTP");
            return r.json();
        })
        .then(vendas => {
            const tbody = document.getElementById("tabelaVendas");
            tbody.innerHTML = "";

            vendas.forEach(v => {
                const tr = document.createElement("tr");
                tr.innerHTML = `
                    <td>${v.vendedor}</td>
                    <td>${v.valor.toFixed(2)}</td>
                `;
                tbody.appendChild(tr);
            });
        })
        .catch(err => {
            console.error("Erro ao carregar vendas:", err);
            const tbody = document.getElementById("tabelaVendas");
            tbody.innerHTML = `<tr><td colspan="2">Erro ao carregar vendas.</td></tr>`;
        });
}

function carregarComissoes() {
    fetch(`${API_BASE}/api/comissoes`)
        .then(r => {
            if (!r.ok) throw new Error("Erro HTTP");
            return r.json();
        })
        .then(comissoes => {
            const tbody = document.getElementById("tabelaComissoes");
            tbody.innerHTML = "";

            comissoes.forEach(c => {
                const tr = document.createElement("tr");
                tr.innerHTML = `
                    <td>${c.vendedor}</td>
                    <td>${c.comissaoTotal.toFixed(2)}</td>
                `;
                tbody.appendChild(tr);
            });
        })
        .catch(err => {
            console.error("Erro ao carregar comissões:", err);
            const tbody = document.getElementById("tabelaComissoes");
            tbody.innerHTML = `<tr><td colspan="2">Erro ao carregar comissões.</td></tr>`;
        });
}

function movimentarEstoque() {
    const codigo = parseInt(document.getElementById("movCodigo").value || "0", 10);
    const quantidade = parseInt(document.getElementById("movQuantidade").value || "0", 10);
    const resultado = document.getElementById("resultadoMov");

    if (!codigo || !quantidade) {
        resultado.textContent = "Informe código e quantidade.";
        return;
    }

    const body = {
        codigoProduto: codigo,
        descricao: "movimentação via frontend",
        quantidade: quantidade
    };

    fetch(`${API_BASE}/api/estoque/movimentar`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(body)
    })
        .then(r => r.json())
        .then(res => {
            if (res.mensagem) {
                resultado.textContent = res.mensagem;
                return;
            }
            resultado.textContent = `Movimentação realizada. Estoque final: ${res.estoqueFinal}`;
            carregarEstoque();
        })
        .catch(err => {
            console.error("Erro ao movimentar estoque:", err);
            resultado.textContent = "Erro ao movimentar estoque.";
        });
}

function calcularJuros() {
    const valor = parseFloat(document.getElementById("jurosValor").value || "0");
    const data = document.getElementById("jurosData").value;
    const resultado = document.getElementById("resultadoJuros");

    if (!valor || !data) {
        resultado.textContent = "Informe o valor e a data.";
        return;
    }

    const url = `${API_BASE}/api/juros?valor=${encodeURIComponent(valor)}&data=${encodeURIComponent(data)}`;

    fetch(url)
        .then(r => r.json())
        .then(res => {
            if (res.mensagem) {
                resultado.textContent = res.mensagem;
                return;
            }

            resultado.textContent =
                `Vencimento: ${res.dataVencimento} | ` +
                `Juros: R$ ${res.juros.toFixed(2)} | ` +
                `Total: R$ ${res.valorTotal.toFixed(2)}`;
        })
        .catch(err => {
            console.error("Erro ao calcular juros:", err);
            resultado.textContent = "Erro ao calcular juros.";
        });
}

document.addEventListener("DOMContentLoaded", () => {
    mostrarAba("estoque");

    carregarEstoque();
    carregarVendas();
    carregarComissoes();
});
window.mostrarAba = mostrarAba;
window.movimentarEstoque = movimentarEstoque;
window.calcularJuros = calcularJuros;
window.carregarEstoque = carregarEstoque;
window.carregarVendas = carregarVendas;
window.carregarComissoes = carregarComissoes;
