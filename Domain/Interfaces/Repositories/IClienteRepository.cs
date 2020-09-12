using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> Listar();
        Task CadastrarAsync(Cliente cliente);
        void Editar(Cliente cliente);
        Task<Cliente> ObterAsync(string cpf);
        void Excluir(int? id);
    }
}
