using Library.EFRepository.Contexts;
using Library.Entity;
using Library.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Library.EFRepository
{
    public class AdressDAO : IEFRepository<Adress>
    {
        public AdressDAO(IDbContextTransaction? transaction = null, DbConnection dbConnection = null)
        {
            Context = new AdressContext(dbConnection);
            Transaction = transaction;
        }

        private AdressContext Context { get; set; }

        public IDbContextTransaction? Transaction { get; set; }


        public bool Save(Adress adress)
        {
            try
            {
                IniciateTransactionIfNotExists();
                Context.Add(adress);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(Adress entity)
        {
            try
            {
                Context.Remove(entity);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Adress entity)
        {
            try
            {
                Context.Update(entity);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void rollback()
        {
            Transaction.Rollback();
        }

        private void IniciateTransactionIfNotExists()
        {
            if (Transaction == null)
                Transaction = Context.Database.BeginTransaction();
            else
                Context.Database.UseTransaction(Transaction.GetDbTransaction());
        }

        public IDbContextTransaction? GetTransaction<IDbContextTransaction>()
        {
            return (IDbContextTransaction?)Transaction;
        }

        public IDbContextTransaction GetTransaction()
        {
            return Transaction;
        }

        public DbConnection GetConnection()
        {
            return Context.Database.GetDbConnection();
        }
    }
}
