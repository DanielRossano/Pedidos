using Microsoft.AspNetCore.Mvc;
using Pedidos.Application.Requests.ProdutosRequest;
using Pedidos.Application.Services;

namespace Pedidos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoAppService _service;

        public ProdutosController(IProdutoAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> Listar([FromQuery] ListarProdutosRequest request)
        {
            var result = await _service.ListarProdutosAsync(request);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ListarPorId(int id)
        {
            var result = await _service.ListarProdutoAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarProdutoRequest request)
        {
            await _service.AtualizarProdutoAsync(id, request);
            return Ok(new { mensagem = "Produto atualizado com sucesso.", produtoId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarProdutoRequest request)
        {
            var result = await _service.CriarProdutoAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Excluir(int id)
        {
            await _service.ExcluirProdutoAsync(id);
            return Ok(new { mensagem = "Produto excluído.", produtoId = id });
        }
    }
}