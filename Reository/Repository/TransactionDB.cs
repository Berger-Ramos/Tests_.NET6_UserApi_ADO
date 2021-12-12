using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
