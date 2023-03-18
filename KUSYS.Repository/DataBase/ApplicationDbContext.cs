using KUSYS.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KUSYS.Repository.DataBase
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         //   modelBuilder.Entity<StudentCourse>()
         //.HasKey(bc => new { bc.StudentId, bc.CourseId});
         //   modelBuilder.Entity<StudentCourse>()
         //       .HasOne(bc => bc.Student)
         //       .WithMany(b => b.StudentCourses)
         //       .HasForeignKey(bc => bc.StudentId);
         //   modelBuilder.Entity<StudentCourse>()
         //       .HasOne(bc => bc.Course)
         //       .WithMany(b => b.StudentCourses)
         //       .HasForeignKey(bc => bc.CourseId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
