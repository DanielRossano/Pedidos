using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Infrastructure.Data;

namespace Pedidos.Infrastructure.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto?> ListarPorIdAsync(int id);
        Task<List<Produto>> ListarAsync(int pagina, int tamanhoPagina);
        Task CriarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task ExcluirAsync(int id);
        Task SaveChangesAsync();
    }

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly PedidosDbContext _context;

        public ProdutoRepository(PedidosDbContext context)
        {
            _context = context;
        }

        public async Task<Produto?> ListarPorIdAsync(int id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }   

        public async Task<List<Produto>> ListarAsync(int pagina, int tamanhoPagina)
        {
            var query = _context.Produtos.AsQueryable();

            return await query
                .OrderByDescending(p => p.Id)
                .Skip((pagina -1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task CriarAsync(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirAsync(int id)
        {
            var produto = await ListarPorIdAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

