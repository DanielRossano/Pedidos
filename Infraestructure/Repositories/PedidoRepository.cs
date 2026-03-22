using Microsoft.EntityFrameworkCore;
using Pedidos.Domain.Entities;
using Pedidos.Infrastructure.Data;

namespace Pedidos.Infrastructure.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido?> ListarPorIdAsync(int id);
        Task<List<Pedido>> ListarAsync();
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

        public async Task<List<Pedido>> ListarAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Produtos)
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
