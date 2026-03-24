using System;

namespace Pedidos.Domain.Entities
{
    public class ProdutosPedido
    {
        public int Id { get; private set; }
        public int PedidoId { get; private set; }
        public int ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        // Navegação (opcional no domínio puro, mas útil p/ EF)
        public Produto? Produto { get; private set; }

        private ProdutosPedido() { }

        public ProdutosPedido(int pedidoId, int produtoId, int quantidade, decimal precoUnitario)
        {
            if (quantidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantidade), "Quantidade deve ser maior que zero.");

            if (precoUnitario < 0)
                throw new ArgumentOutOfRangeException(nameof(precoUnitario), "Preço unitário não pode ser negativo.");

            PedidoId = pedidoId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public void AlterarQuantidade(int novaQuantidade)
        {
            if (novaQuantidade <= 0)
                throw new ArgumentOutOfRangeException(nameof(novaQuantidade), "Quantidade deve ser maior que zero.");

            Quantidade = novaQuantidade;
        }
    }
}
