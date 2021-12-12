namespace Library.RepositoryInterface
{
    public interface IRepository<T> where T : class
    {
        bool Save(T entity);
        bool Delete(T entity);
        bool Update(T entity);

    }
}
