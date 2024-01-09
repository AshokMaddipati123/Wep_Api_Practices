using Microsoft.EntityFrameworkCore;
using StudentAPI.Model;

namespace StudentAPI.Datacontext
{
    public class StudentContext:DbContext
    {

        public StudentContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Students> Student { get; set; }
       
    }
}
