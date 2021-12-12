using Library.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public abstract class BaseRepository
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.GetConnection();
            }
        }

        public static void ExecuteProcedure(List<SqlParameter> parameters, string procedureName, TransactionDB transactionDB)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            try
            {
                SqlCommand cmd = null;

                if (transactionDB != null)
                {
                    transactionDB.SetConnectionAndTransaction(con);
                    cmd = new SqlCommand(procedureName, con, transactionDB.GetTransaction());
                }
                else
                {
                    con.Open();
                    new SqlCommand(procedureName, con, null);
                }
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                cmd.ExecuteNonQuery();

                if (transactionDB == null)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (transactionDB != null)
                {
                    transactionDB.Commit();
                }
                else
                {
                    con.Close();
                }
                throw ex;
            }
        }
    }
}
