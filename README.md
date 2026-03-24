# Pedidos API

API para gerenciamento de `Pedidos` e `Produtos`, construída com ASP.NET Core e Entity Framework Core.

## Stack
- `.NET 10`
- `ASP.NET Core Web API`
- `Entity Framework Core` + `SQL Server`
- `Swagger`

## Estrutura
- `Presentation/Controllers`: endpoints HTTP da API.
- `Application/Services`: regras de aplicação e casos de uso.
- `Application/Requests`: contratos de entrada (payloads).
- `Application/DTOs`: contratos de saída (respostas).
- `Domain/Entities`: modelo de domínio (regras e estado).
- `Domain/Enums`: enums de domínio (status de pedido).
- `Infrastructure/Data`: contexto do banco (`DbContext`).
- `Infrastructure/Repositories`: acesso a dados.
- `Program.cs`: configuração banco, controllers e Swagger.

## Pré-requisitos
- SDK do `.NET 10`
- SQL Server

## 1) Criar o banco de dados
Execute o script abaixo no SQL Server:

```sql
CREATE DATABASE Pedidos;
GO
USE Pedidos;
GO
CREATE TABLE Produtos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(200) NOT NULL,
    Descricao NVARCHAR(1000) NULL,
    Preco DECIMAL(18, 2) NOT NULL,
    CONSTRAINT CHK_Produto_Preco CHECK (Preco >= 0)
);
GO
CREATE TABLE Pedidos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DataCriacao DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    DataFechamento DATETIME2 NULL,
    Status INT NOT NULL,
    CONSTRAINT CHK_Pedido_Status CHECK (Status IN (0, 1)),
    CONSTRAINT CHK_Pedido_DataFechamento CHECK (DataFechamento IS NULL OR DataFechamento >= DataCriacao)
);
GO
CREATE TABLE ProdutosPedido (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId INT NOT NULL,
    ProdutoId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(18, 2) NOT NULL,
    CONSTRAINT FK_ProdutosPedido_Pedido FOREIGN KEY (PedidoId)
        REFERENCES Pedidos(Id) ON DELETE CASCADE,
    CONSTRAINT FK_ProdutosPedido_Produto FOREIGN KEY (ProdutoId)
        REFERENCES Produtos(Id),
    CONSTRAINT CHK_ItemPedido_Quantidade CHECK (Quantidade > 0),
    CONSTRAINT CHK_ItemPedido_PrecoUnitario CHECK (PrecoUnitario >= 0)
);
GO
```

## 2) Configurar a conexão
A conexão é configurável no arquivo `appsettings.Database.json`.

Exemplo com IP local (instância `sql2019`):

```json
{
  "ConnectionStrings": {
    "PedidosConnection": "Server=192.168.0.19\\sql2019;Database=Pedidos;User Id=sa;Password=123;TrustServerCertificate=True;Integrated Security=False;Persist Security Info=True;MultipleActiveResultSets=True;Encrypt=False;"
  }
}
```


## 3) Rodar a aplicação
Na raiz do projeto:

```bash
dotnet restore
dotnet run
```

## 4) Testar a API
- Swagger UI: `https://localhost:<porta>/swagger`

## Endpoints principais
- `GET /api/produtos`
- `GET /api/produtos/{id}`
- `POST /api/produtos`
- `PUT /api/produtos/{id}`
- `DELETE /api/produtos/{id}`
- `GET /api/pedidos`
- `GET /api/pedidos/{id}`
- `POST /api/pedidos`
- `POST /api/pedidos/{id}/produtos/adicionar`
- `POST /api/pedidos/{id}/produtos/remover`
- `PUT /api/pedidos/{id}/fechar`
