using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTOs;
using Domain.Interfaces.Services;
using Domain.Models;
using Infra.CrossCutting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        // GET: api/<EnderecoController>
        [HttpPost]
        [Route("listar")]
        public async Task<IActionResult> Listar([FromBody] EnderecoDto enderecoDto)
        {
            Response<EnderecoDto> response;

            try
            {
                response = await _enderecoService.ListarAsync(enderecoDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Ocorreu um erro ao tentar cadastrar o cliente. Favor aguardar uns minutos e tentar novamente." });
            }

            return Ok(response.ListaDto);
        }

        // POST api/<EnderecoController>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] EnderecoDto enderecoDto)
        {
            Response response;

            try
            {
                response = await _enderecoService.CadastrarAsync(enderecoDto);

                if (!response.Sucesso)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensagem = response.Mensagem });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Ocorreu um erro ao tentar cadastrar o endereço. Favor aguardar uns minutos e tentar novamente." });
            }

            return Ok(new { mensagem = response.Mensagem });
        }

        //    // PUT api/<EnderecoController>/5
        [HttpPut]
        [Route("atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] EnderecoDto enderecoDto)
        {
            Response response;

            try
            {
                response = await _enderecoService.AtualizarAsync(id, enderecoDto);

                if (!response.Sucesso)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensagem = response.Mensagem });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Ocorreu um erro ao tentar atualizar o endereço. Favor aguardar uns minutos e tentar novamente." });
            }

            return Ok(new { mensagem = response.Mensagem });
        }

        // DELETE api/<EnderecoController>/5
        [HttpDelete]
        [Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            Response response;

            try
            {
                response = await _enderecoService.ExcluirAsync(id);

                if (!response.Sucesso)
                {
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensagem = response.Mensagem });
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Ocorreu um erro ao tentar excluir o endereço. Favor aguardar uns minutos e tentar novamente." });
            }

            return Ok(new { mensagem = response.Mensagem });
        }
    }
}
