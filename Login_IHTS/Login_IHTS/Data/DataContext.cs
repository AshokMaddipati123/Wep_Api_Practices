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
                .UseSqlServer("Server=DESKTOP-GEUANKH;Database=TIMETRACKER;Trusted_Connection=true;TrustServerCertificate=True");
        }

        public DbSet<User> USERDETAILS => Set<User>();
        public DbSet<ProjectModel> PROJECTNAMES { get; set; }
        public DbSet<LocationModel> LOCATIONS { get; set; }
       
        public DbSet<TaskModel> TASKDETAILS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<ProjectModel>().HasKey(p => p.ProjectId);

            modelBuilder.Entity<LocationModel>().HasKey(l => l.LocationId);
       
            modelBuilder.Entity<TaskModel>().HasKey(t => t.TaskId);
        }



    }
}
