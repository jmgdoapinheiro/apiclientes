using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Interfaces.Services;
using Infra.CrossCutting;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ClienteController>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] ClienteDto cliente)
        {
            Response response;

            try
            {
                response = await _clienteService.CadastrarAsync(cliente);

                if (!response.Sucesso)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensagem = response.Mensagem });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Ocorreu um erro ao tentar cadastrar o cliente. Favor aguardar uns minutos e tentar novamente." });
            }

            return Ok(new { mensagem = response.Mensagem });
        }

        // PUT api/<ClienteController>/5
        [HttpPut]
        [Route("atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] ClienteDto cliente)
        {
            Response response;

            try
            {
                response = await _clienteService.AtualizarAsync(id, cliente);

                if (!response.Sucesso)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensagem = response.Mensagem });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Ocorreu um erro ao tentar atualizar o cliente. Favor aguardar uns minutos e tentar novamente." } );
            }

            return Ok(new { mensagem = response.Mensagem });
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
