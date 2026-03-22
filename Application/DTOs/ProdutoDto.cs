using Pedidos.Application.Dtos.ProdutoPedido;


namespace Pedidos.Application.Dtos.ProdutoDto
{

    public class ProdutoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
    }
    public class CriarProdutoDto
    {
        public int Id { get; set; }
    }
}