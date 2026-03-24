using Pedidos.Application.Dtos.PedidoDto;
using Pedidos.Application.Dtos.ProdutoDto;
using Pedidos.Application.Requests.ProdutosRequest;
using Pedidos.Domain.Entities;
using Pedidos.Infrastructure.Repositories;
namespace Pedidos.Application.Services
{
    public interface IProdutoAppService
    {
        Task<List<ProdutoDto>> ListarProdutosAsync(ListarProdutosRequest request);
        Task<ProdutoDto?> ListarProdutoAsync(int Id);
        Task<CriarProdutoDto> CriarProdutoAsync(CriarProdutoRequest request);
        Task<ProdutoDto> AtualizarProdutoAsync(int Id, AtualizarProdutoRequest request);
        Task ExcluirProdutoAsync(int Id);
    }

    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoAppService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoDto>> ListarProdutosAsync(ListarProdutosRequest request)
        {
            var produtos = await _produtoRepository.ListarAsync(request.Pagina, request.TamanhoPagina);
            return produtos.Select(MapToDto).ToList();
        }
        public async Task<ProdutoDto?> ListarProdutoAsync(int Id)
        {
            var produto = await _produtoRepository.ListarPorIdAsync(Id);
            if (produto == null) 
                return null;
            return MapToDto(produto);
        }
        public async Task<CriarProdutoDto>CriarProdutoAsync(CriarProdutoRequest request)
        {
            var produto = new Produto(request.Nome, request.Preco, request.Descricao);         
            await _produtoRepository.CriarAsync(produto);

            return new CriarProdutoDto { Id = produto.Id };
        }
        public async Task<ProdutoDto>AtualizarProdutoAsync(int Id, AtualizarProdutoRequest request)
        {
            var produto = await _produtoRepository.ListarPorIdAsync(Id)
                        ?? throw new Exception("Produto não encontrado");
            produto.AtualizarProduto(request.Nome, request.Preco, request.Descricao);
            await _produtoRepository.AtualizarAsync(produto);

            return new ProdutoDto { Id = produto.Id };
        }

        public async Task ExcluirProdutoAsync(int Id)
        {
            var produto = await _produtoRepository.ListarPorIdAsync(Id)
                        ?? throw new Exception("Produto não encontrado");
            await _produtoRepository.ExcluirAsync(Id);
        }
        private static ProdutoDto MapToDto(Produto produto)
        {
            return new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco
            };
        }

    }

}
