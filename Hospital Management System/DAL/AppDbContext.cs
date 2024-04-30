using Hospital_Management_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Management_System.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Department> Departments { get; set; }
    }
}
