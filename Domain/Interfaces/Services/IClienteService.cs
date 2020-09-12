using Domain.DTOs;
using Infra.CrossCutting;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Response> CadastrarAsync(ClienteDto cliente);
        Task<Response> AtualizarAsync(long id, ClienteDto cliente);
    }
}
