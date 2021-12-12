using Library.Utils;
using System.Data;
using System.Data.SqlClient;

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

                transactionDB.SetConnectionAndTransaction(con);
                cmd = new SqlCommand(procedureName, con, transactionDB == null ? null : transactionDB.GetTransaction());

                if (transactionDB == null)
                    con.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddRange(parameters.ToArray());
                cmd.ExecuteNonQuery();

                if (transactionDB == null)
                    con.Close();
            }
            catch (Exception ex)
            {
                if (transactionDB != null)
                    transactionDB.Commit();
                else
                    con.Close();

                throw ex;
            }
        }
    }
}
