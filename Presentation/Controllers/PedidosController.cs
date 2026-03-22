using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Requests;
using Pedidos.Application.Services;

namespace Pedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoAppService _service;

        public PedidoController(IPedidoAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Listar([FromQuery] ListarPedidosRequest request)
        {
            var result = await _service.ListarPedidosAsync(request);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ListarPorId(int id)
        {
            var result = await _service.ListarPedidoAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Criar()
        {
            var result = await _service.IniciarNovoPedidoAsync();
            return Ok(result);
        }

        [HttpPost("{id:int}/produtos/adicionar")]
        public async Task<IActionResult> AdicionarProduto(int id, [FromBody] AdicionarProdutoRequest request)
        {
            await _service.AdicionarProdutoAsync(id, request);
            return Ok(new { mensagem = "Produto adicionado ao pedido com sucesso.", pedidoId = id, produtoId = request.ProdutoId });
        }

        [HttpPost("{id:int}/produtos/remover")]
        public async Task<IActionResult> RemoverProduto(int id, [FromBody] RemoverProdutoRequest request)
        {
            await _service.RemoverProdutoAsync(id, request);
            return Ok(new { mensagem = "Produto removido do pedido com sucesso.", pedidoId = id, produtoId = request.ProdutoId });
        }

        [HttpPut("{id:int}/fechar")]
        public async Task<IActionResult> Fechar(int id)
        {
            await _service.FecharPedidoAsync(id);
            return Ok(new { mensagem = "Pedido fechado com sucesso.", pedidoId = id });
        }
    }
}