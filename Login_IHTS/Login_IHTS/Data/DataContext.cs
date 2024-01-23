using API_TimeTracker.Models;
using System.Collections.Generic;

namespace API_TimeTracker.Data
{
    public class DataContext : DbContext
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseSqlServer("Server=DESKTOP-GEUANKH;Database=userdb;Trusted_Connection=true;TrustServerCertificate=True");
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<TimePeriodModel> TimePeriods { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectModel>().HasKey(p => p.ProjectId);
          
            modelBuilder.Entity<LocationModel>().HasKey(l => l.LocationId);
            modelBuilder.Entity<TimePeriodModel>().HasKey(tp => tp.TimePeriodId);
            modelBuilder.Entity<TaskModel>().HasKey(t => t.TaskId);
        }

    }
}
