using Microsoft.EntityFrameworkCore;
using Student_Dashboard_Api.Data.Entities;

namespace Student_Dashboard_Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasKey(student => student.Id);
        }
    }
}
