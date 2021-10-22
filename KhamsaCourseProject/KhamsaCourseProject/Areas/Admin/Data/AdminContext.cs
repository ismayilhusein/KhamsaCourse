using KhamsaCourseProject.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Data
{
    public class AdminContext :DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options):base(options)
        {
        }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<StudentGroup> StudentGroups { get; set; }
        public DbSet<StudentType> StudentTypes { get; set; }
        public DbSet<StudentPayment> Payments { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<PaymentCategory> PaymentCategories { get; set; }
        public DbSet<StudentContract> Contracts { get; set; }
        public DbSet<ContractType> ContractTypes { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public DbSet<ExtraDebt> ExtraDebts { get; set; }
        public DbSet<ExtraPayment> ExtraPayments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmployeeContract> EmployeeContracts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
            .HasOne(p => p.Contract)
            .WithOne(b => b.Student).HasForeignKey<StudentContract>(b => b.StudentId);

            modelBuilder.Entity<Employee>()
           .HasOne(p => p.EmployeeContract)
           .WithOne(b => b.Employee).HasForeignKey<EmployeeContract>(b => b.EmployeeId);
        }

    }
}
