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

    botoes.forEach(btn => {
        const acao = btn.getAttribute("onclick") || "";
        if (acao.includes(`'${id}'`)) {
            btn.classList.add("ativo");
        }
    });
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
                if (p.estoque <= 50) {
                    tr.classList.add("low-stock");
                }

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
            console.error("Deu algo de errado no estoque :( :", err);
            document.getElementById("totalItens").textContent =
                "Algo deu erro, melhor ver se a API está rodando!";
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
            tbody.innerHTML =
                `<tr><td colspan="2">Cade a tabela de vendas?.</td></tr>`;
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
            tbody.innerHTML =
                `<tr><td colspan="2">Não consegui calcular as comissões agora.</td></tr>`;
        });
}

function movimentarEstoque() {
    const codigo = parseInt(document.getElementById("movCodigo").value || "0", 10);
    const quantidade = parseInt(document.getElementById("movQuantidade").value || "0", 10);
    const resultado = document.getElementById("resultadoMov");

    if (!codigo || !quantidade) {
        resultado.textContent = "Preencha o código e a quantidade.";
        return;
    }

    const body = {
        codigoProduto: codigo,
        descricao: "Movimentação realizada pelo painel uhuul",
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

            resultado.textContent = `Movimentação registrada uhuul. Estoque final: ${res.estoqueFinal}`;
            carregarEstoque();
        })
        .catch(err => {
            console.error("Erro ao movimentar estoque (ah não):", err);
            resultado.textContent = "Não consegui registrar essa movimentação agora.";
        });
}

function calcularJuros() {
    const valor = parseFloat(document.getElementById("jurosValor").value || "0");
    const rawDate = document.getElementById("jurosData").value;
    const resultado = document.getElementById("resultadoJuros");

    if (!valor || !rawDate) {
        resultado.textContent = "Informe o valor e selecione a data de vencimento para calcular.";
        return;
    }

    const [ano, mes, dia] = rawDate.split("-");
    const dataFormatada = `${dia}-${mes}-${ano}`;

    const url = `${API_BASE}/api/juros?valor=${encodeURIComponent(valor)}&data=${encodeURIComponent(dataFormatada)}`;

    fetch(url)
        .then(r => r.json())
        .then(res => {
            if (res.mensagem) {
                resultado.textContent = res.mensagem;
                return;
            }

            resultado.textContent =
                `Vencimento: ${res.dataVencimento} | ` +
                `Juros acumulado: R$ ${res.juros.toFixed(2)} | ` +
                `Total com juros: R$ ${res.valorTotal.toFixed(2)}`;
        })
        .catch(err => {
            console.error("Erro ao calcular juros:", err);
            resultado.textContent = "Não consegui calcular os juros agora. Tente novamente em alguns instantes.";
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
