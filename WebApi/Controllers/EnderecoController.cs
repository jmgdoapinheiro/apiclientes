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
            var response = await _enderecoService.ListarAsync(enderecoDto);

            if(!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, response.ListaDto);
        }

        // POST api/<EnderecoController>
        [HttpPost]
        [Route("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] EnderecoDto enderecoDto)
        {
            var response = await _enderecoService.CadastrarAsync(enderecoDto);
            
            if (!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });
        }

        //    // PUT api/<EnderecoController>/5
        [HttpPut]
        [Route("atualizar/{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] EnderecoDto enderecoDto)
        {
            var response = await _enderecoService.AtualizarAsync(id, enderecoDto);

            if (!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });
        }

        // DELETE api/<EnderecoController>/5
        [HttpDelete]
        [Route("excluir/{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            var response = await _enderecoService.ExcluirAsync(id);

            if (!response.Sucesso)
                return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });

            return StatusCode(response.StatusCode, new { mensagem = response.Mensagem });
        }
    }
}
