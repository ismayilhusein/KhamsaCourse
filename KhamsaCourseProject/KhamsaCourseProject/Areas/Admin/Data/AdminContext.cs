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

    }
}
