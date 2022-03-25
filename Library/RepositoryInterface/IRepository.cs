using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Library.RepositoryInterface
{
    public interface IEFRepository<T> where T : class
    {
        bool Save(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        void Commit();
        void rollback();
        IDbContextTransaction GetTransaction();
        DbConnection GetConnection();
    }
}
