using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.ValueObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Infra
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private string _connectionString;

        public EnderecoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ApiClientesDbSettings:ConnectionString").Value;
        }

        public async Task AtualizarAsync(Endereco endereco)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "update endereco set logradouro=@logradouro, bairro=@bairro, cidade=@cidade, estado=@estado where id_cliente=@idCliente";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idCliente", endereco.IdCliente);
                cmd.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
                cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
                cmd.Parameters.AddWithValue("@estado", endereco.Estado);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task CadastrarAsync(Endereco endereco)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "insert into endereco (id_cliente,logradouro,bairro,cidade,estado)values(@idCliente, @logradouro, @bairro, @cidade, @estado)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idCliente", endereco.IdCliente);
                cmd.Parameters.AddWithValue("@logradouro", endereco.Logradouro);
                cmd.Parameters.AddWithValue("@bairro", endereco.Bairro);
                cmd.Parameters.AddWithValue("@cidade", endereco.Cidade);
                cmd.Parameters.AddWithValue("@estado", endereco.Estado);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task ExcluirAsync(long idCliente)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "delete from endereco where id_cliente=@idCliente";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<EnderecoDto>> ListarAsync(Cliente cliente, Endereco endereco)
        {
            IList<EnderecoDto> enderecos = new List<EnderecoDto>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "select nome, cpf, logradouro, bairro, cidade, estado from cliente c inner join endereco e on c.id = e.id_cliente";
                string filter = "";

                if (!string.IsNullOrWhiteSpace(cliente.Cpf.Numero))
                    filter += "cpf like " + "'%" + cliente.Cpf.Numero + "%'";

                if (!string.IsNullOrWhiteSpace(cliente.Nome))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "nome like " + "'%" + cliente.Nome + "%'";
                }

                if (!string.IsNullOrWhiteSpace(endereco.Logradouro))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "logradouro like " + "'%" + endereco.Logradouro + "%'";
                }

                if (!string.IsNullOrWhiteSpace(endereco.Bairro))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "bairro like " + "'%" + endereco.Bairro + "%'";
                }

                if (!string.IsNullOrWhiteSpace(endereco.Cidade))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "cidade like " + "'%" + endereco.Cidade + "%'";
                }

                if (!string.IsNullOrWhiteSpace(endereco.Estado))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "estado like " + "'%" + endereco.Estado + "%'";
                }

                if (!string.IsNullOrEmpty(filter))
                    comandoSQL += " where " + filter;

                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    enderecos.Add(
                        new EnderecoDto
                        {
                            Cliente = reader[0].ToString(),
                            Cpf = reader[1].ToString(),
                            Logradouro = reader[2].ToString(),
                            Bairro = reader[3].ToString(),
                            Cidade = reader[4].ToString(),
                            Estado = reader[5].ToString(),
                        }
                    );
                }

                reader.Close();
            }

            return enderecos.AsEnumerable();
        }

        public async Task<Endereco> ObterAsync(long idCliente)
        {
            Endereco endereco = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string comandoSQL = "select top 1 id_cliente, logradouro, bairro, cidade, estado from endereco where id_cliente = @idCliente";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    endereco = new Endereco(Convert.ToInt64(reader[0]), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString());
                }

                reader.Close();
            }

            return endereco;

        }
    }
}
