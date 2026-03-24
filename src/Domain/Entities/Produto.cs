using System;

namespace Pedidos.Domain.Entities
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public decimal Preco { get; private set; }

        private Produto() { }

        public Produto(string nome, decimal preco, string? descricao = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto é obrigatório.", nameof(nome));

            if (preco < 0)
                throw new ArgumentOutOfRangeException(nameof(preco), "Preço não pode ser negativo.");

            Nome = nome;
            Preco = preco;
            Descricao = descricao;
        }

        public void AtualizarProduto(string nome, decimal preco, string? descricao = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do produto é obrigatório.", nameof(nome));
            if (preco < 0)
                throw new ArgumentOutOfRangeException(nameof(preco), "Preço não pode ser negativo.");
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
        }

    }
}
