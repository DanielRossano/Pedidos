using Pedidos.Domain.Enums;

namespace Pedidos.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataFechamento { get; private set; }
        public StatusPedido Status { get; private set; }

        private readonly List<ProdutosPedido> _produtos = new();
        public IReadOnlyCollection<ProdutosPedido> Produtos => _produtos.AsReadOnly();

        public Pedido()
        {
            DataCriacao = DateTime.UtcNow;
            Status = StatusPedido.Aberto;
        }

        public void AdicionarProduto(int produtoId, int quantidade, decimal precoUnitario)
        {
            if (Status == StatusPedido.Fechado)
                throw new InvalidOperationException("Não é possível adicionar produtos a um pedido fechado.");

            var existente = _produtos.FirstOrDefault(i => i.ProdutoId == produtoId);

            if (existente != null)
            {
                existente.AlterarQuantidade(existente.Quantidade + quantidade);
            }
            else
            {
                var produto = new ProdutosPedido(Id, produtoId, quantidade, precoUnitario);
                _produtos.Add(produto);
            }
        }

        public void RemoverProduto(int produtoId, int quantidade)
        {
            if (Status == StatusPedido.Fechado)
                throw new InvalidOperationException("Não é possível remover itens de um pedido fechado.");

            var existente = _produtos.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (existente == null)
                throw new InvalidOperationException("Produto não encontrado neste pedido.");

            if (quantidade >= existente.Quantidade)
                _produtos.Remove(existente);
            else
                existente.AlterarQuantidade(existente.Quantidade - quantidade);
        }

        public void Fechar()
        {
            if (Status == StatusPedido.Fechado)
                return;

            if (!_produtos.Any())
                throw new InvalidOperationException("O pedido não pode fechado sem fechado sem produtos");

            Status = StatusPedido.Fechado;
            DataFechamento = DateTime.UtcNow;
        }
    }
}
