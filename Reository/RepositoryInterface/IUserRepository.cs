using Library.Entity;
using Repository.Repository;

namespace Library.RepositoryInterface
{
    public interface IUserRepository : IRepository<User>, IDisposable
    {
        User GetUserByName(string userName);
    }
}
