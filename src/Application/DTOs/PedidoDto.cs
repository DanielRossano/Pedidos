namespace Pedidos.Application.DTOs
{

    public class PedidoDto
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataFechamento { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<ProdutosPedidoDto> Itens { get; set; } = new();
    }

    public class IniciarPedidoResponseDto
    {
        public int Id { get; set; }
    }
}
