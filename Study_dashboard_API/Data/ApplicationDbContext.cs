using Microsoft.EntityFrameworkCore;
using Study_dashboard_API.Models;

namespace Study_dashboard_API.Data
{
    // Database context for the application
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Event> Events { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Subject)
                .WithMany()
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Subject>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Kuba1", Password = "kuba1111" },
                new User { UserId = 2, Name = "Kuba2", Password = "kuba2222" },
                new User { UserId = 3, Name = "Kuba3", Password = "kuba3333" },
                new User { UserId = 4, Name = "Kuba4", Password = "kuba4444" },
                new User { UserId = 5, Name = "Kuba5", Password = "kuba5555" },
                new User { UserId = 6, Name = "Kuba6", Password = "kuba6666" });
            modelBuilder.Entity<Subject>().HasData(
                new Subject { SubjectId = 1, Name = "Smiw", Ects = 5, PriorityLevel = PriorityLevelEnum.Medium, UserId = 1 },
                new Subject { SubjectId = 2, Name = "Smiw lab", Ects = 2, PriorityLevel = PriorityLevelEnum.Medium, UserId = 1 },
                new Subject { SubjectId = 3, Name = "Pk 5", Ects = 3, PriorityLevel = PriorityLevelEnum.Medium, UserId = 1 },
                new Subject { SubjectId = 4, Name = "Programowanie 4", Ects = 1, PriorityLevel = PriorityLevelEnum.Medium, UserId = 2 },
                new Subject { SubjectId = 5, Name = "PBD", Ects = 2, PriorityLevel = PriorityLevelEnum.Medium, UserId = 2 },
                new Subject { SubjectId = 6, Name = "Java_web", Ects = 1, PriorityLevel = PriorityLevelEnum.Medium, UserId = 3 },
                new Subject { SubjectId = 7, Name = "Tm", Ects = 1, PriorityLevel = PriorityLevelEnum.Medium, UserId = 3 });
            modelBuilder.Entity<Event>()
                .HasData(
                new Event { EventId = 1, Name = "sprawdzian z pic", Description = "Nie zdam! xd", PriorityLevel = PriorityLevelEnum.Medium, Date = new DateTime(2025, 3, 10), SubjectId = 1, UserId = 1 },
                new Event { EventId = 2, Name = "wejsciowka", Description = "wejsciowka z avr", PriorityLevel = PriorityLevelEnum.Medium, Date = new DateTime(2025, 3, 8), SubjectId = 2, UserId = 1 },
                new Event { EventId = 3, Name = "projekt", Description = "literaki", PriorityLevel = PriorityLevelEnum.Medium, Date = new DateTime(2025, 3, 17), SubjectId = 3, UserId = 1 },
                new Event { EventId = 4, Name = "kolokwium", Description = "Nie zdam! xd", PriorityLevel = PriorityLevelEnum.High, Date = new DateTime(2025, 3, 11), SubjectId = 7, UserId = 3 });
        }
    }
}
