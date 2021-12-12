using System.Configuration;
using UserApi.Controllers;
using System;
using System.Collections.Generic;

using System.Data.SqlClient;

namespace UserApi.Repository
{
    public class UserDAO
    {
        public string ConnectionString { get => ConnectionString; set => ConnectionString = "Data Source=DESKTOP-1IC09TM;Initial Catalog=UserDB;Integrated Security=True"; }
        public static bool SaveUser( User user) 
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "Insert into Funcionarios (Nome,Cidade,Departamento,Sexo) 
                                                        Values(@Nome, @Cidade, @Departamento, @Sexo)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", funcionario.Nome);
                cmd.Parameters.AddWithValue("@Cidade", funcionario.Cidade);
                cmd.Parameters.AddWithValue("@Departamento", funcionario.Departamento);
                cmd.Parameters.AddWithValue("@Sexo", funcionario.Sexo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            return true;
        }

    }
}
