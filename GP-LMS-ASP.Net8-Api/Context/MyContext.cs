using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<StudentGroup> StudentGroups { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<CourseSubjectElements> CourseSubjectElements { get; set; }
        public virtual DbSet<CourseSubject> CourseSubjects { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
    .HasOne(a => a.Student)
    .WithMany(e => e.Attendances)
    .HasForeignKey(a => a.StudentId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseSubjectElements>()
    .HasOne(cse => cse.Group)
    .WithMany(g => g.CourseSubjectElements)
    .HasForeignKey(cse => cse.GroupId);

            base.OnModelCreating(modelBuilder);

            // Apply Global Query Filter for Soft Delete
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Parent>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Groups>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<CourseSubject>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<CourseSubjectElements>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Fee>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Expense>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}