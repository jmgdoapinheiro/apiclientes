﻿using Domain.DTOs;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDto>> ListarAsync(Cliente cliente);
        Task CadastrarAsync(Cliente cliente);
        Task Atualizar(long id, Cliente cliente);
        Task<Cliente> ObterAsync(string cpf);
        Task<Cliente> ObterAsync(long id);
        void Excluir(int? id);
    }
}
