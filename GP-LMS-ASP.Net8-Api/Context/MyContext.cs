using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.EntityFrameworkCore;


namespace GP_LMS_ASP.Net8_Api.Context
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Parent> Parents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply Global Query Filter for Soft Delete
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Parent>().HasQueryFilter(p => !p.IsDeleted);
        }

    }
}
