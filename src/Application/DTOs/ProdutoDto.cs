namespace Pedidos.Application.DTOs
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