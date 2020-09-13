using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infra
{
    public class ClienteRepository : IClienteRepository
    {
        private string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ApiClientesDbSettings:ConnectionString").Value;
        }

        public async Task<IEnumerable<ClienteDto>> ListarAsync(Cliente cliente)
        {
            IList<ClienteDto> clientes = new List<ClienteDto>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "select nome, cpf, data_nascimento, idade from cliente";
                string filter = "";

                if (!string.IsNullOrWhiteSpace(cliente.Cpf.Numero))
                    filter += "cpf like " + "'%" + cliente.Cpf.Numero + "%'";

                if (!string.IsNullOrWhiteSpace(cliente.Nome))
                {
                    if(!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "nome like " + "'%" + cliente.Nome + "%'";
                }

                if (!string.IsNullOrEmpty(filter))
                    comandoSQL += " where " + filter;
                
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    DateTime dataNascimento;

                    DateTime.TryParse(reader[2].ToString(), out dataNascimento);

                    cliente = new Cliente(reader[0].ToString(), reader[1].ToString(), dataNascimento);

                    clientes.Add(
                        new ClienteDto { 
                           Nome = reader[0].ToString(), 
                           Cpf = reader[1].ToString(), 
                           DataNascimento = dataNascimento.ToString("dd/MM/yyyy HH:mm:ss") 
                        }
                    );
                }

                reader.Close();
            }

            return clientes.AsEnumerable();
        }

        public async Task CadastrarAsync(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "insert into cliente (nome,cpf,idade,data_nascimento)values(@nome, @cpf, @idade, @dataNascimento)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.Cpf.Numero);
                cmd.Parameters.AddWithValue("@idade", cliente.Idade);
                cmd.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task Atualizar(long id, Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "update cliente set nome=@nome, cpf=@cpf, idade=@idade, data_nascimento=@dataNascimento where id=@id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf", cliente.Cpf.Numero);
                cmd.Parameters.AddWithValue("@idade", cliente.Idade);
                cmd.Parameters.AddWithValue("@dataNascimento", cliente.DataNascimento);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ExcluirAsync(long id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "delete from cliente where id=@id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<Cliente> ObterAsync(string cpf)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "select top 1 nome, cpf, data_nascimento, id from cliente where cpf = @cpf";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@cpf", cpf);

                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    DateTime dataNascimento;

                    DateTime.TryParse(reader[2].ToString(), out dataNascimento);

                    cliente = new Cliente(reader[0].ToString(), reader[1].ToString(), dataNascimento);
                    cliente.AdiocionarId(Convert.ToInt64(reader[3]));
                }

                reader.Close();
            }

            return cliente;
        }

        public async Task<Cliente> ObterAsync(long id)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "select top 1 nome, cpf, data_nascimento from cliente where id = @id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    DateTime dataNascimento;

                    DateTime.TryParse(reader[2].ToString(), out dataNascimento);

                    cliente = new Cliente(reader[0].ToString(), reader[1].ToString(), dataNascimento);
                }

                reader.Close();
            }

            return cliente;
        }
    }
}
