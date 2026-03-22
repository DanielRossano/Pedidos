namespace Pedidos.Application.Requests
{
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