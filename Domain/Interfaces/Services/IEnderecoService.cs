using Domain.DTOs;
using Infra.CrossCutting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IEnderecoService
    {
        Task<Response<EnderecoDto>> ListarAsync(EnderecoDto enderecoDto);
        Task<Response> CadastrarAsync(EnderecoDto enderecoDto);
        Task<Response> AtualizarAsync(long idCliente, EnderecoDto enderecoDto);
        Task<Response> ExcluirAsync(long idCliente);
    }
}
