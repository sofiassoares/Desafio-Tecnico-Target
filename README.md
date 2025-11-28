<div align="center">
  <h2> Projeto: Desafio Técnico – Target Sistemas </h2>

  <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" />
  <img src="https://img.shields.io/badge/xUnit-5D2C86?style=for-the-badge&logo=.net&logoColor=white" />
  <img src="https://img.shields.io/badge/HTML5-E44D26?style=for-the-badge&logo=html5&logoColor=white" />
  <img src="https://img.shields.io/badge/CSS3-1572B6?style=for-the-badge&logo=css3&logoColor=white" />
  <img src="https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black" />

  <br />
  <br />
  <a href="https://ibb.co/Z67NzqWg"><img src="https://i.ibb.co/3yQkmgBF/image.png" alt="image" border="0" />
</div>

---

## 💡 Visão Geral

Este repositório implementa o desafio técnico proposto pela **Target Sistemas**.

O objetivo foi construir uma solução completa, com:

- Aplicação **console em .NET/C#**
- **API REST** em .NET expondo os dados do domínio
- **Dashboard web** (HTML, CSS e JavaScript) consumindo a API

A solução simula um sistema de gestão trabalhando com:

- **Vendas**
- **Comissões**
- **Estoque**
- **Juros de títulos em atraso**

Com foco em:

- organização de código (camadas, services, repositories)  
- clareza das regras de negócio  
- experiência visual inspirada no site da Target.

---

## ✨ Funcionalidades

### 🧮 Aplicação Console (.NET)

Menu principal:

- `1` – Calcular comissão de vendedores  
- `2` – Movimentar estoque (entrada/saída)  
- `3` – Calcular juros de títulos em atraso  
- `0` – Sair  

Principais pontos:

- Lê dados de vendas e estoque a partir de arquivos `.json`
- Aplica regras de negócio em serviços dedicados
- Atualiza o estoque e mostra o resultado na tela
- Calcula juros com base na data de vencimento informada

---

### 🌐 API REST

Endpoints principais:

- `GET /api/estoque`  
  Lista produtos e respectivos estoques.

- `POST /api/estoque/movimentar`  
  Recebe uma movimentação (código + quantidade) e atualiza o estoque.

- `GET /api/vendas`  
  Retorna as vendas registradas.

- `GET /api/comissoes`  
  Calcula e retorna o total de comissão por vendedor.

- `GET /api/juros?valor={valor}&data={dd-MM-yyyy}`  
  Calcula juros acumulados e valor total de um título.

A API reutiliza as mesmas regras de negócio da aplicação console.

---

### 📊 Dashboard Web

Interface em abas, consumindo a API via `fetch`:

- **Estoque**
  - Visão geral: total de itens em estoque
  - Tabela de produtos (código, descrição, quantidade)
  - Destaque em vermelho para produtos com estoque baixo
  - Form para movimentar estoque (entrada/saída) com feedback na tela

- **Vendas**
  - Tabela com vendedor e valor da venda
  - Dados vindos diretamente da API

- **Comissões**
  - Tabela com total de comissão por vendedor
  - Mesma lógica da aplicação console, exposta via API

- **Juros**
  - Campo de valor original
  - Campo de data (input `type="date"` com calendário)
  - Exibe:
    - data de vencimento
    - juros acumulado
    - valor total com juros

---

## Stack

### Back-end

- **.NET SDK**
- **C#**
- **API REST** (minimal API)
- **System.Text.Json** para leitura e escrita de arquivos JSON
- **xUnit** para testes automatizados

### Front-end

- **HTML5**
- **CSS3**
- **JavaScript** (DOM + `fetch`, sem framework)

### Persistência

- Arquivos `.json` simulando um banco de dados:
  - `estoque.json`
  - `vendas.json`

---

## 📂 Estrutura do Projeto

```text
DesafioTecnico/
├─ src/
│  ├─ DesafioTecnico.App/      # Aplicação console (.NET)
│  ├─ DesafioTecnico.Api/      # API REST (.NET)
├─ tests/
│  ├─ DesafioTecnico.Tests/    # Testes automatizados (xUnit)
├─ frontend/
│  ├─ index.html               # Dashboard web
│  ├─ style.css                # Estilos do dashboard
│  ├─ app.js                   # Integração com a API
└─ README.md
```

> Ajuste o nome da pasta do front se estiver usando outro (por exemplo, `web/` em vez de `frontend/`).

---

### ✅ Pré-requisitos

- **.NET SDK** instalado  
- Navegador 
- Alguma forma de rodar um servidor estático pro front (por exemplo, **Live Server** no VS Code ou `npx serve`)

---

### Rodando os testes

Na raiz do projeto:

```bash
dotnet test
```

Isso executa a suíte de testes em `tests/DesafioTecnico.Tests`.

---

### Rodando a aplicação console

```bash
dotnet run --project src/DesafioTecnico.App
```

Você verá o menu com:

- 1 – Comissão  
- 2 – Estoque  
- 3 – Juros  
- 0 – Sair  

#### Observação sobre arquivos JSON

Os arquivos `vendas.json` e `estoque.json` ficam na pasta:

```text
src/DesafioTecnico.App/Data/
```

No `.csproj` do console, configure assim (se ainda não fez):

```xml
<ItemGroup>
  <None Include="Data\*.json" CopyToOutputDirectory="PreserveNewest" />
</ItemGroup>
```

E no `Program.cs`, use caminhos relativos:

```csharp
var baseDir = AppContext.BaseDirectory;
var dataDir = Path.Combine(baseDir, "Data");

var caminhoVendas  = Path.Combine(dataDir, "vendas.json");
var caminhoEstoque = Path.Combine(dataDir, "estoque.json");
```

Assim, quem clonar o repositório não precisa ajustar paths na mão.

---

### 🚀 Subindo a API

Em outro terminal, na raiz do projeto:

```bash
dotnet run --project src/DesafioTecnico.Api
```

A API deve subir em um endereço local, por exemplo:

```text
http://localhost:5259
```

Esse endereço é usado no front em `frontend/app.js`:

```javascript
const API_BASE = "http://localhost:5259";
```

Se a porta for diferente, é só ajustar esse valor em `API_BASE`.

---

### Rodando o Front-end

Na pasta `frontend/` (ou o nome que você estiver usando):

- Abra o `index.html` com o **Live Server** (VS Code)  
  ou use qualquer servidor estático simples.


```bash
cd frontend

```

Depois acesse o endereço informado (por exemplo, `http://localhost:3000`).

> Importante: a **API precisa estar rodando** antes de usar o dashboard, senão as chamadas `fetch` vão falhar.

---

## 📃 Uso

### Fluxo recomendado para avaliação

1. Clonar o repositório
2. Rodar os testes (`dotnet test`)
3. Rodar a aplicação console (opcional, pra ver a versão em linha de comando)
4. Subir a API (`dotnet run --project src/DesafioTecnico.Api`)
5. Abrir o front (`frontend/index.html` via Live Server ou servidor estático)
6. Navegar pelas abas e testar:

   - **Estoque**: conferir lista de produtos, total e movimentação  
   - **Vendas**: visualizar as vendas cadastradas  
   - **Comissões**: ver a comissão total por vendedor  
   - **Juros**: informar valor + data de vencimento e calcular juros

---

## Detalhes de Implementação

### Organização do código .NET

- **Models**  
  Representam as entidades de domínio:
  - `Produto`
  - `Venda`
  - `Movimentacao`

- **Repositories**  
  Responsáveis pela leitura e escrita dos arquivos JSON:
  - `EstoqueRepository`
  - `VendasRepository`

- **Services**
  - `ComissaoService`: regra de cálculo da comissão por venda
  - `EstoqueService`: movimentação de estoque em memória (entrada/saída)
  - `JurosService`: cálculo de juros com base na data de vencimento

- **Console / Core**
  - `Menu`: controla o fluxo da aplicação console

- **API**
  - Reaproveita os mesmos serviços e repositórios
  - Expõe as funcionalidades via HTTP para o front-end

- **app.js**
  - `mostrarAba(id)`: controla qual aba está visível e qual botão está ativo
  - `carregarEstoque()`: consome `GET /api/estoque`, atualiza tabela e total
  - `carregarVendas()`: consome `GET /api/vendas`, renderiza tabela de vendas
  - `carregarComissoes()`: consome `GET /api/comissoes`, mostra comissão por vendedor
  - `movimentarEstoque()`: envia `POST /api/estoque/movimentar` com código/quantidade
  - `calcularJuros()`: lê valor + data (`input type="date"`), converte para `dd-MM-yyyy` e consome `GET /api/juros`

---

## Contato

- GitHub: [**sofiassoares**](https://github.com/sofiassoares)  

> Desenvolvido para o desafio técnico da **Target Sistemas**.
