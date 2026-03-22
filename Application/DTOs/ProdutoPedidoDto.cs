
namespace Pedidos.Application.Dtos.ProdutoPedido
{
    public class ProdutosPedidoDto
    {
        public int ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }

}
