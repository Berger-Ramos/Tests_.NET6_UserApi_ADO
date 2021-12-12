using Library.Entity;

namespace Library.RepositoryInterface
{
    public interface IUserRepository : IRepository<User>, IDisposable
    {
        User GetUserByName(string userName);
    }
}
