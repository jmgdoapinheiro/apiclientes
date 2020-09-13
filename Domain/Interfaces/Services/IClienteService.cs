using Domain.DTOs;
using Domain.Models;
using Infra.CrossCutting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ListarClienteDto>> ListarAsync(ClienteDto cliente);
        Task<Response> CadastrarAsync(ClienteDto cliente);
        Task<Response> AtualizarAsync(long id, ClienteDto cliente);
        Task<Response> ExcluirAsync(long idCliente);
    }
}
