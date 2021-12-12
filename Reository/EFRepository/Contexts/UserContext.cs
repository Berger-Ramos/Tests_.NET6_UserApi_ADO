using Library.Entity;
using Library.Utils;
using Microsoft.EntityFrameworkCore;

namespace Library.EFRepository.Contexts
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ConfigurationManager.GetConnection());
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.HasKey(e => e.Id)
        //            .HasName("UserID");

        //        entity.HasKey(e => e.Name);

        //    });
        //}
    }
}
