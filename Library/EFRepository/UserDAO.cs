﻿using Library.EFRepository.Contexts;
using Library.Entity;
using Library.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Library.EFRepository
{
    public class UserDAO : IUserRepository
    {
        public UserDAO(IDbContextTransaction? transaction = null )
        {
            Context = new UserContext();
            Transaction = transaction;
            Connection = Context.Database.GetDbConnection();
        }

        private UserContext Context { get; set; }

        public DbConnection Connection { get; set; }

        public IDbContextTransaction? Transaction { get; set; }


        public bool Save(User entity)
        {
            try
            {
                IniciateTransactionIfNotExists();
                Context.Add(entity);
                Context.SaveChanges(false);
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
            return Context.User.OrderByDescending(u => u.Name).Take(100).ToList();
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
        }

        public DbConnection GetConnection()
        {
            return Context.Database.GetDbConnection();
        }

        public IDbContextTransaction GetTransaction()
        {
            return Transaction;
        }

        public User GetUser(string userName, string password)
        {
            return Context.User.Where(user => user.Name == userName && user.Password == password).FirstOrDefault();
        }
    }
}
