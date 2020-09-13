using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/<ClienteController>
        [HttpPost]
        [Route("listar")]
        public async Task<IActionResult> Listar([FromBody] ClienteDto cliente)
        {
            var response = await _clienteService.ListarAsync(cliente);

            if (!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, response.ListaDto);
        }

        // POST api/<ClienteController>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] ClienteDto cliente)
        {
            var response = await _clienteService.CadastrarAsync(cliente);

            if (!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });
        }

        // PUT api/<ClienteController>/5
        [HttpPut]
        [Route("atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ClienteDto cliente)
        {
            var response = await _clienteService.AtualizarAsync(id, cliente);

            if (!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete]
        [Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var response = await _clienteService.ExcluirAsync(id);

            if (!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });
        }
    }
}
