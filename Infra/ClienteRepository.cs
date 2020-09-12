using Domain.Interfaces.Repositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Infra
{
    public class ClienteRepository : IClienteRepository
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=apiclientes;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task CadastrarAsync(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
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
            using (SqlConnection con = new SqlConnection(connectionString))
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

        public void Excluir(int? id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cliente> Listar()
        {
            throw new NotImplementedException();
        }

        public async Task<Cliente> ObterAsync(string cpf)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "select top 1 nome, cpf, data_nascimento from cliente where cpf = @cpf";
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
                }

                reader.Close();
            }

            return cliente;
        }

        public async Task<Cliente> ObterAsync(long id)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
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
