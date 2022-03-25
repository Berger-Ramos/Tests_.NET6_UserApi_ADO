using Library.Entity;
using Library.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.EFRepository.Contexts
{
    public class AdressContext : DbContext
    {
        public AdressContext(DbConnection connection = null)
        {
            Connection = connection;
        }
        public DbConnection? Connection { get; set; }
        public virtual DbSet<Adress> Adress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (Connection == null)
                    optionsBuilder.UseSqlServer(ConfigurationManager.GetConnection());
                else
                    optionsBuilder.UseSqlServer(Connection);
            }
               
        }
    }
}
