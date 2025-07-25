﻿using GP_LMS_ASP.Net8_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GP_LMS_ASP.Net8_Api.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<GroupPaymentCycle> GroupPaymentCycles { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<CourseSubjectElements> CourseSubjectElements { get; set; }
        public DbSet<CourseSubject> CourseSubjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendance>()
    .HasOne(a => a.Student)
    .WithMany()
    .HasForeignKey(a => a.StudentId)
    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseSubjectElements>()
                .HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CourseSubjectElements>()
                .HasOne(e => e.Group)
                .WithMany()
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);

            // Apply Global Query Filter for Soft Delete
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Parent>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Level>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Groups>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<CourseSubject>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<CourseSubjectElements>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Fee>().HasQueryFilter(e => !e.IsDeleted);
        }
    }
}