using Library.EFRepository.Contexts;
using Library.Entity;
using Library.RepositoryInterface;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EFRepository
{
    public class UserDAO : IUserRepository
    {
        public UserDAO()
        {
            Context = new UserContext();
        }

        public UserContext Context { get; set; }

        public bool Save(User entity)
        {
            try
            {
                Context.Add(entity);
                Context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User GetUserByName(string userName)
        {
            return Context.User.Where(x => x.Name == userName).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return Context.User.Select(x => x).ToList();
        }
        public bool Delete(User entity)
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

        public bool Update(User entity)
        {
            try
            {
                Context.User.Update(entity);
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
    }
}
