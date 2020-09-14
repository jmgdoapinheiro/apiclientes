using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra
{
    public class ApiClientesContext: IApiClientesContext
    {
        private string _connectionString;

        public ApiClientesContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ApiClientesDbSettings:ConnectionString").Value;
        }

        public MySqlConnection CriarConexao()
        {
            return new MySqlConnection(_connectionString);
        }

        public MySqlCommand CriarComando(string comando, MySqlConnection con)
        {
            return new MySqlCommand(comando, con);
        }
    }
}
