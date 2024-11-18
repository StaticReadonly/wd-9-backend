using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.DbConn
{
    public class DbContext1 : DbContext
    {
        public DbContext1(DbContextOptions<DbContext1> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(Menu.ConfigureEntity);
        }
    }
}
