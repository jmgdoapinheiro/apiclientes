using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.ValueObjects;
using Infra.Interfaces;
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
        private readonly IApiClientesContext _apiClientesContext;

        public EnderecoRepository(IApiClientesContext apiClientesContext)
        {
            _apiClientesContext = apiClientesContext;
        }

        public async Task AtualizarAsync(Endereco endereco)
        {
            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "update endereco set logradouro=@logradouro, bairro=@bairro, cidade=@cidade, estado=@estado where id_cliente=@idCliente";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

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
            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "insert into endereco (id_cliente,logradouro,bairro,cidade,estado)values(@idCliente, @logradouro, @bairro, @cidade, @estado)";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

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
            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "delete from endereco where id_cliente=@idCliente";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<EnderecoDto>> ListarAsync(EnderecoDto enderecoDto)
        {
            IList<EnderecoDto> enderecos = new List<EnderecoDto>();

            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "select nome, cpf, logradouro, bairro, cidade, estado from cliente c inner join endereco e on c.id = e.id_cliente";
                string filter = "";

                if (!string.IsNullOrWhiteSpace(enderecoDto.Cpf))
                    filter += "cpf like " + "'%" + enderecoDto.Cpf + "%'";

                if (!string.IsNullOrWhiteSpace(enderecoDto.Cliente))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "nome like " + "'%" + enderecoDto.Cliente + "%'";
                }

                if (!string.IsNullOrWhiteSpace(enderecoDto.Logradouro))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "logradouro like " + "'%" + enderecoDto.Logradouro + "%'";
                }

                if (!string.IsNullOrWhiteSpace(enderecoDto.Bairro))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "bairro like " + "'%" + enderecoDto.Bairro + "%'";
                }

                if (!string.IsNullOrWhiteSpace(enderecoDto.Cidade))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "cidade like " + "'%" + enderecoDto.Cidade + "%'";
                }

                if (!string.IsNullOrWhiteSpace(enderecoDto.Estado))
                {
                    if (!string.IsNullOrEmpty(filter))
                        filter += " and ";

                    filter += "estado like " + "'%" + enderecoDto.Estado + "%'";
                }

                if (!string.IsNullOrEmpty(filter))
                    comandoSQL += " where " + filter;

                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;

                con.Open();
                var reader = await cmd.ExecuteReaderAsync();

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

            using (var con = _apiClientesContext.CriarConexao())
            {
                string comandoSQL = "select id_cliente, logradouro, bairro, cidade, estado from endereco where id_cliente = @idCliente limit 1";
                var cmd = _apiClientesContext.CriarComando(comandoSQL, con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                con.Open();
                var reader = await cmd.ExecuteReaderAsync();

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
