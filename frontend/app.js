// Carregar o JSON de estoque
fetch("estoque.json")
    .then(response => response.json())
    .then(data => {
        
        const produtos = data.estoque;
        const tabela = document.getElementById("tabelaEstoque");
        const totalItens = document.getElementById("totalItens");

        let soma = 0;

        produtos.forEach(p => {
            soma += p.estoque;

            const linha = document.createElement("tr");
            linha.innerHTML = `
                <td>${p.codigoProduto}</td>
                <td>${p.descricaoProduto}</td>
                <td>${p.estoque}</td>
            `;
            tabela.appendChild(linha);
        });

        totalItens.textContent = `Total de itens no estoque: ${soma}`;
    })
    .catch(error => {
        console.error("Erro ao carregar estoque:", error);
        document.getElementById("totalItens").textContent = "Erro ao carregar dados.";
    });
