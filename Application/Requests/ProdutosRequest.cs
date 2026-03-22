namespace Pedidos.Application.Requests.ProdutosRequest
{
    public class CriarProdutoRequest
    {

        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
    }

    public class AtualizarProdutoRequest
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}
