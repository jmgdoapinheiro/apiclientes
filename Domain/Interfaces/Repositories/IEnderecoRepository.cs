using Domain.DTOs;
using Domain.Models;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<IEnumerable<EnderecoDto>> ListarAsync(EnderecoDto enderecoDto);
        Task CadastrarAsync(Endereco endereco);
        Task AtualizarAsync(Endereco endereco);
        Task<Endereco> ObterAsync(long idCliente);
        Task ExcluirAsync(long idCliente);
    }
}
