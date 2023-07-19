using lista7_zad1.Models;
using Microsoft.EntityFrameworkCore;

namespace lista7_zad1.Context
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<People> Osoby { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeVal> GradeVals { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Subject> Subjects { get; set; }

    }
}
