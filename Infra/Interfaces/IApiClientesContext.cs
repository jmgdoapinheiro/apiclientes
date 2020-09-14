using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Interfaces
{
    public interface IApiClientesContext
    {
        MySqlConnection CriarConexao();
        MySqlCommand CriarComando(string comando, MySqlConnection con);
    }
}
