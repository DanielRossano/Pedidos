using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Enums;
using Pedidos.Infrastructure.Data;

namespace Pedidos.Infrastructure.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido?> ListarPorIdAsync(int id);
        Task<List<Pedido>> ListarAsync(StatusPedido? status, int pagina, int tamanhoPagina);
        Task AdicionarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task SaveChangesAsync();
    }

    public class PedidoRepository : IPedidoRepository
    {
        private readonly PedidosDbContext _context;

        public PedidoRepository(PedidosDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido?> ListarPorIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Produtos)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pedido>> ListarAsync(StatusPedido? status, int pagina, int tamanhoPagina)
        {
            var query = _context.Pedidos
                .Include(p => p.Produtos)
                .ThenInclude(i => i.Produto)
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(p => p.Status == status.Value);
            }

            return await query
                .OrderByDescending(p => p.Id)
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
