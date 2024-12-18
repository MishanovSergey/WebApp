using DBConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace DBConnect
{
    public class ApplicationContext : DbContext
    {
        public DbSet<NewsInfoModel> News => Set<NewsInfoModel>();
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\SQLLiteDB\\webApp.db");
        }
    }
}
