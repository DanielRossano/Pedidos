using System.ComponentModel.DataAnnotations;
using Pedidos.Domain.Enums;

namespace Pedidos.Application.Requests
{
    public class ListarPedidosRequest
    {
        [Range(1, int.MaxValue)]
        public int Pagina { get; set; } = 1;

        [Range(1, 100)]
        public int TamanhoPagina { get; set; } = 10;

        public StatusPedido? Status { get; set; }
    }

    public class AdicionarProdutoRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }

    public class RemoverProdutoRequest
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}