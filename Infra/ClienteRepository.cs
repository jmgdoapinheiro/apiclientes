using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.ValueObjects;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infra
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IApiClientesContext _apiClientesContext;

        public ClienteRepository(IApiClientesContext apiClientesContext)
        {
            _apiClientesContext = apiClientesContext;
        }

        public async Task<IEnumerable<ListarClienteDto>> ListarAsync(ClienteDto clienteDto)
        {
            IList<ListarClienteDto> clientes = new List<ListarClienteDto>();

            //using (MySqlConnection con = new MySqlConnection(_connectionString))
            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "select nome, cpf, idade from cliente";
                string filter = "";

                if (!string.IsNullOrWhiteSpace(clienteDto.Cpf))
                    filter += "cpf like " + "'%" + Cpf.DesformatarNumero(clienteDto.Cpf) + "%'";

                if (!string.IsNullOrWhiteSpace(clienteDto.Nome))
                {
                    if(!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "nome like " + "'%" + clienteDto.Nome + "%'";
                }

                if (!string.IsNullOrEmpty(filter))
                    comandoSQL += " where " + filter;

                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    clientes.Add(
                        new ListarClienteDto
                        { 
                           Nome = reader[0].ToString(), 
                           Cpf = reader[1].ToString(), 
                           Idade = reader[2].ToString()
                        }
                    );
                }

                reader.Close();
            }

            return clientes.AsEnumerable();
        }

        public async Task CadastrarAsync(Cliente cliente)
        {
            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "insert into cliente (nome,cpf,idade)values(@nome, @cpf, @idade)";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.Cpf.NumeroDesformatado);
                cmd.Parameters.AddWithValue("@idade", cliente.Idade);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Atualizar(long id, Cliente cliente)
        {
            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "update cliente set nome=@nome, cpf=@cpf, idade=@idade where id=@id";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.Cpf.NumeroDesformatado);
                cmd.Parameters.AddWithValue("@idade", cliente.Idade);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ExcluirAsync(long id)
        {
            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "delete from cliente where id=@id";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<Cliente> ObterAsync(string cpf)
        {
            Cliente cliente = null;

            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "select nome, cpf, id from cliente where cpf = @cpf limit 1";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@cpf", cpf);

                con.Open();
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    cliente = new Cliente(reader[0].ToString(), reader[1].ToString(), DateTime.Now);
                    cliente.AdiocionarId(Convert.ToInt64(reader[2]));
                }

                reader.Close();
            }

            return cliente;
        }

        public async Task<Cliente> ObterAsync(long id)
        {
            Cliente cliente = null;

            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "select nome, cpf from cliente where id = @id limit 1";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                var reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    cliente = new Cliente(reader[0].ToString(), reader[1].ToString(), DateTime.Now);
                }

                reader.Close();
            }

            return cliente;
        }
    }
}
