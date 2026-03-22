using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pedidos.Domain.Entities;
using Pedidos.Domain.Enums;

namespace Pedidos.Infrastructure.Data
{
    public class PedidosDbContext : DbContext
    {
        public PedidosDbContext(DbContextOptions<PedidosDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<ProdutosPedido> ProdutosPedido => Set<ProdutosPedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(builder =>
            {
                builder.ToTable("Produtos");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();
                builder.Property(p => p.Nome)
                    .HasMaxLength(200)
                    .IsRequired();
                builder.Property(p => p.Descricao)
                    .HasMaxLength(1000);
                builder.Property(p => p.Preco)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });

            modelBuilder.Entity<Pedido>(builder =>
            {
                builder.ToTable("Pedidos");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                    .ValueGeneratedOnAdd()
                    .UseIdentityColumn();
                builder.Property(p => p.DataCriacao)
                    .HasColumnType("datetime2")
                    .IsRequired();

                builder.Property(p => p.DataFechamento)
                    .HasColumnType("datetime2");

                builder.Property(p => p.Status)
                    .HasConversion<int>()
                    .IsRequired();

                builder.HasMany(p => p.Produtos)
                       .WithOne()
                       .HasForeignKey("PedidoId")
                       .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProdutosPedido>(builder =>
            {
                builder.ToTable("ProdutosPedido");
                builder.HasKey(i => i.Id);

                builder.Property(i => i.Quantidade)
                    .IsRequired();

                builder.Property(i => i.PrecoUnitario)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                builder.HasOne(i => i.Produto)
                    .WithMany()
                    .HasForeignKey(i => i.ProdutoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
