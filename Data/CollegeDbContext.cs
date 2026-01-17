using Microsoft.EntityFrameworkCore;
using Winterhold_College_Course_Registration_System.Models;

namespace Winterhold_College_Course_Registration_System.Data
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions<CollegeDbContext> options)
            : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Staff>()
                .Property(s => s.Role)
                .HasConversion<string>();

            modelBuilder.Entity<Staff>()
                .Property(s => s.Department)
                .HasConversion<string>();

            modelBuilder.Entity<Student>()
                .Property(s => s.Major)
                .HasConversion<string>();

            modelBuilder.Entity<Course>()
                .Property(c => c.Department)
                .HasConversion<string>();

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Grade)
                .HasConversion<string>();

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Professor)
                .WithMany(s => s.CoursesTaught)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
