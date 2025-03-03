using Microsoft.EntityFrameworkCore;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Kuba1", Password = "kuba1111" },
                new User { UserId = 2, Name = "Kuba2", Password = "kuba2222" },
                new User { UserId = 3, Name = "Kuba3", Password = "kuba3333" },
                new User { UserId = 4, Name = "Kuba4", Password = "kuba4444" },
                new User { UserId = 5, Name = "Kuba5", Password = "kuba5555" },
                new User { UserId = 6, Name = "Kuba6", Password = "kuba6666" });
        }
    }
}
