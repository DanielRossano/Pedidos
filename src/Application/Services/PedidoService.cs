using Pedidos.Application.DTOs;
using Pedidos.Application.Requests;
using Pedidos.Domain.Entities;
using Pedidos.Infrastructure.Repositories;

namespace Pedidos.Application.Services
{
    public interface IPedidoAppService
    {
        Task<IniciarPedidoResponseDto> IniciarNovoPedidoAsync();
        Task AdicionarProdutoAsync(int pedidoId, AdicionarProdutoRequest request);
        Task RemoverProdutoAsync(int pedidoId, RemoverProdutoRequest request);
        Task FecharPedidoAsync(int pedidoId);
        Task<List<PedidoDto>> ListarPedidosAsync(ListarPedidosRequest request);
        Task<PedidoDto?> ListarPedidoAsync(int pedidoId);
    }

    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;
        readonly IProdutoRepository _produtoRepository;

        public PedidoAppService(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }
        public async Task<IniciarPedidoResponseDto> IniciarNovoPedidoAsync()
        {
            var pedido = new Pedido();
            await _pedidoRepository.AdicionarAsync(pedido);

            return new IniciarPedidoResponseDto { Id = pedido.Id };
        }

        public async Task AdicionarProdutoAsync(int pedidoId, AdicionarProdutoRequest request)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(pedidoId)
                         ?? throw new InvalidOperationException("Pedido não encontrado.");

            var produto = await _produtoRepository.ListarPorIdAsync(request.ProdutoId)
                          ?? throw new InvalidOperationException("Produto não encontrado.");

            pedido.AdicionarProduto(produto.Id, request.Quantidade, produto.Preco);

            await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task RemoverProdutoAsync(int pedidoId, RemoverProdutoRequest request)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(pedidoId)
                         ?? throw new InvalidOperationException("Pedido não encontrado.");

            pedido.RemoverProduto(request.ProdutoId, request.Quantidade);

            await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task FecharPedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(pedidoId)
                         ?? throw new InvalidOperationException("Pedido não encontrado.");

            pedido.Fechar();

            await _pedidoRepository.AtualizarAsync(pedido);
        }

        public async Task<List<PedidoDto>> ListarPedidosAsync(ListarPedidosRequest request)
        {
            var pedidos = await _pedidoRepository.ListarAsync(request.Status, request.Pagina, request.TamanhoPagina);
            return pedidos.Select(MapToDto).ToList();
        }

        public async Task<PedidoDto?> ListarPedidoAsync(int pedidoId)
        {
            var pedido = await _pedidoRepository.ListarPorIdAsync(pedidoId);
            return pedido == null ? null : MapToDto(pedido);
        }

        private static PedidoDto MapToDto(Pedido pedido)
        {
            return new PedidoDto
            {
                Id = pedido.Id,
                DataCriacao = pedido.DataCriacao,
                DataFechamento = pedido.DataFechamento,
                Status = pedido.Status.ToString().ToLowerInvariant(),
                Itens = pedido.Produtos.Select(i => new ProdutosPedidoDto
                {
                    ProdutoId = i.ProdutoId,
                    NomeProduto = i.Produto?.Nome,
                    Quantidade = i.Quantidade,
                    PrecoUnitario = i.PrecoUnitario
                }).ToList()
            };
        }
    }
}
