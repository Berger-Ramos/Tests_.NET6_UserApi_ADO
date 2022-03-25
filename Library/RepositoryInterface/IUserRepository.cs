using Library.Entity;

namespace Library.RepositoryInterface
{
    public interface IUserRepository : IEFRepository<User>, IDisposable
    {
        User GetUserByName(string userName);

        User GetUser(string userName, string password);
    }
}
