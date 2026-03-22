using Microsoft.EntityFrameworkCore;
using Pedidos.Application.Services;
using Pedidos.Infrastructure.Data;
using Pedidos.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PedidosConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("A ConnectionString não foi configurada.");
}

builder.Services.AddDbContext<PedidosDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoAppService, ProdutoAppService>();
builder.Services.AddScoped<IPedidoAppService, PedidoAppService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
