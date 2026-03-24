using System.ComponentModel.DataAnnotations;

namespace Pedidos.Application.Requests
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
    public class ListarProdutosRequest
    {
        [Range(1, int.MaxValue)]
        public int Pagina { get; set; } = 1;

        [Range(1, 100)]
        public int TamanhoPagina { get; set; } = 10;
    }
}
