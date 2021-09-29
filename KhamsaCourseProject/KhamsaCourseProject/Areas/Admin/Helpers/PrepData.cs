using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Helpers
{
    public static class PrepData
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                AdminContext _db = scope.ServiceProvider.GetRequiredService<AdminContext>();
                SeedData(_db);
            }

        }
        public static void SeedData(AdminContext db)
        {
            if (!db.StudentClasses.Any())
            {
                db.StudentClasses.AddRange(new List<StudentClass>() {
                new StudentClass{ Name = "1-ci sinif" },
                new StudentClass{ Name = "2-ci sinif" },
                new StudentClass{ Name = "3-ci sinif" },
                new StudentClass{ Name = "4-ci sinif" },
                new StudentClass{ Name = "5-ci sinif" },
                new StudentClass{ Name = "6-ci sinif" },
                new StudentClass{ Name = "7-ci sinif" },
                new StudentClass{ Name = "8-ci sinif" },
                new StudentClass{ Name = "9-ci sinif" },
                new StudentClass{ Name = "10-ci sinif" },
                new StudentClass{ Name = "11-ci sinif" }
                });
            }
            else
            {
                Console.WriteLine($"--> Data already exists context:{nameof(db.StudentClasses)}");
            }
            if (!db.StudentGroups.Any())
            {
                db.StudentGroups.AddRange(new List<StudentGroup>() {
                new StudentGroup{ Name = "1-ci qrup" },
                new StudentGroup{ Name = "2-ci qrup" },
                new StudentGroup{ Name = "3-ci qrup" },
                new StudentGroup{ Name = "4-ci qrup" }
                });
            }
            else
            {
                Console.WriteLine($"--> Data already exists context:{nameof(db.StudentGroups)}");
            }
            if (!db.StudentTypes.Any())
            {
                db.StudentTypes.AddRange(new List<StudentType>() {
                new StudentType{ Name = "Abituryent" },
                new StudentType{ Name = "Magistr" },
                new StudentType{ Name = "Ibtidai sinif" }
                });
            }
            else
            {
                Console.WriteLine($"--> Data already exists context:{nameof(db.StudentTypes)}");
            }
            if (!db.Sectors.Any())
            {
                db.Sectors.Add(new Sector { Name = "Xırdalan", IsActive = 1, Email = "TestEmail", Fax= "TestFax", Phone = "TestPhone" });
            }
            else
            {
                Console.WriteLine($"--> Data already exists context:{nameof(db.Sectors)}");
            }
            if (!db.PaymentTypes.Any())
            {
                db.PaymentTypes.AddRange(new List<PaymentType>() {
                 new PaymentType { Name = "Gəlir" },
                   new PaymentType { Name = "Zərər" }
                });
            }
            else
            {
                Console.WriteLine($"--> Data already exists context:{nameof(db.PaymentTypes)}");
            }
            if (!db.PaymentCategories.Any())
            {
                db.PaymentCategories.AddRange(new List<PaymentCategory>() {
                    new PaymentCategory { Name = "Tələbədən Gəlir" },
                     new PaymentCategory { Name = "İmtahandan Gəlir" },
                    new PaymentCategory { Name = "Nəşriyyatdan Gəlir" },
                     new PaymentCategory { Name = "Əlavə Gəlir" },
                      new PaymentCategory { Name = "Əlavə Xərc" },
                    new PaymentCategory { Name = "Digər" }
                });
            }
            else
            {
                Console.WriteLine($"--> Data already exists context:{nameof(db.PaymentCategories)}");
            }
            db.SaveChanges();
        }
    }
}
