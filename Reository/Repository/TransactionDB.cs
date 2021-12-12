using System.Data.SqlClient;

namespace Library.Repository
{
    public class TransactionDB 
    {
        private  SqlTransaction Transaction { get ; set; }

        private  SqlConnection Con { get; set; }

        private bool WasCommited { get; set; }

        public void Commit()
        {
            Transaction.Commit();
            Con.Close();
        }

        public SqlTransaction GetTransaction()
        {
            return Transaction;
        }

        public void SetConnectionAndTransaction(SqlConnection sqlConnection)
        {
            Con = sqlConnection;
            Con.Open();
            Transaction = Con.BeginTransaction();
            WasCommited = true;
        }

        
    }
}
