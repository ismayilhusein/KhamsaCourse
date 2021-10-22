using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Dtos;
using KhamsaCourseProject.Areas.Admin.Helpers.Enums;
using KhamsaCourseProject.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KhamsaCourseProject.Areas.Admin.Helpers.Enums.DiscountType;

namespace KhamsaCourseProject.Areas.Admin.Helpers
{
    public static class ExConverter
    {
        public static double DiscountedValue(int type, int value, int discountValue)
        {
            switch (type)
            {
                case (int)DTypes.Percentage:
                    return value - (value * ((double)discountValue / 100));
                case (int)DTypes.Number:
                    return value - discountValue;
                default:
                    return 0;
            }
        }
        public static PaginationDto PaginationMethod(int page, int pagecount)
        {
            if (page <= 5 || pagecount <= 9)
            {
                if (pagecount <= 9)
                {
                    return new PaginationDto() { StartPage = 0, PageCount = pagecount, Page = page, EndPage = pagecount - 1 };
                }
                else
                {
                    return new PaginationDto() { StartPage = 0, PageCount = pagecount, Page = page, EndPage = 9 };
                }
            }
            else if (page > pagecount - 5)
            {
                return new PaginationDto() { StartPage = page - 9, PageCount = pagecount, Page = page, EndPage = pagecount - 1 };
            }
            else
            {
                return new PaginationDto() { StartPage = page - 5, PageCount = pagecount, Page = page, EndPage = page + 4 };
            }
        }
        public static List<Student> Filterize
            (
            AdminContext _db,
            string Fullname,
            int id,
            int StudentClasses,
            int StudentGroups,
            int StudentTypes,
            int Status
            )
        {
            List<Student> students = new List<Student>();


            students = _db.Students.Where
                (
                    a =>
                    a.SectorId == id
                )
           .Include(a => a.Sector)
           .Include(a => a.StudentClass)
           .Include(a => a.StudentGroup)
           .Include(a => a.StudentType)
           .Include(a => a.Sector)
           .Include(a => a.Contract)
           .OrderByDescending(a => a.RegistrationDate).ToList();
            if (Status == 0)
            {
                students = students.Where(a => a.IsActive == 1).ToList();
            }
            else
            {
                students = students.Where(a => a.IsActive == 0).ToList();
            }
            if (!string.IsNullOrEmpty(Fullname))
            {
                students = students.Where(a => a.Fullname.Contains(Fullname)).ToList();
            }
            if (StudentClasses > 0)
            {
                students = students.Where(a => a.StudentClassId == StudentClasses).ToList();
            }
            if (StudentGroups > 0)
            {
                students = students.Where(a => a.StudentGroupId == StudentGroups).ToList();
            }
            if (StudentTypes > 0)
            {
                students = students.Where(a => a.StudentTypeId == StudentTypes).ToList();
            }
            return students;
        }
        public static List<Employee> Filterize
                   (
                   AdminContext _db,
                   string Fullname,
                   int id,
                   int EmployeeTypes,
                   int Status
                   )
        {
            List<Employee> students = new List<Employee>();

            students = _db.Employees.Where
                (
                    a =>
                    a.SectorId == id
                )
           .Include(a => a.Sector)
           .Include(a => a.EmployeeType)
           .Include(a => a.Sector)
           .Include(a => a.EmployeeContract)
           .OrderByDescending(a => a.RegistrationDate).ToList();
            if (Status == 0)
            {
                students = students.Where(a => a.IsActive == 1).ToList();
            }
            else
            {
                students = students.Where(a => a.IsActive == 0).ToList();
            }
            if (!string.IsNullOrEmpty(Fullname))
            {
                students = students.Where(a => a.Fullname.Contains(Fullname)).ToList();
            }
            if (EmployeeTypes > 0)
            {
                students = students.Where(a => a.EmployeeTypeId == EmployeeTypes).ToList();
            }
            return students;
        }
        public static EmployeeCreateDto ConvertType(this EmployeeCreateDto dto, Employee emp)
        {
            dto.Fullname = emp.Fullname;
            dto.PhoneNumber = emp.PhoneNumber;
            dto.MobileNumber = emp.MobileNumber;
            dto.RegistrationDate = emp.RegistrationDate;
            dto.SectorId = emp.SectorId;
            dto.EmployeeTypeId = emp.EmployeeTypeId;
            return dto;
        }
        public static List<StatisticsSelectDto> Filterize(int id, DateTime dateFrom, DateTime dateTo, int categoryId, AdminContext db)
        {

            List<StatisticsSelectDto> statistics = (from a in db.Sectors
                                                    select new StatisticsSelectDto
                                                    {
                                                        SectorId = a.Id,
                                                        Benefit = (from d in db.Payments where d.SectorId == a.Id && d.PaymentTypeId == 2 && d.PaymentDate >= dateFrom && d.PaymentDate <= dateTo select d.Value).Sum(),
                                                        Cost = (from d in db.Payments where d.SectorId == a.Id && d.PaymentTypeId == 1 && d.PaymentDate >= dateFrom && d.PaymentDate <= dateTo select d.Value).Sum(),
                                                        CategoryName = "Bütün Kateqoryalar",
                                                        Name = a.Name

                                                    }).ToList();
            if (id > 0)
            {
                statistics = statistics.Where(a => a.SectorId == id).ToList();
            }
            if (categoryId > 0)
            {
                statistics = statistics.Select(a => new StatisticsSelectDto
                {
                    SectorId = a.SectorId,
                    Benefit = (from d in db.Payments where d.SectorId == a.SectorId && d.PaymentTypeId == 2 && d.PaymentDate >= dateFrom && d.PaymentDate <= dateTo && d.CategoryId == categoryId select d.Value).Sum(),
                    Cost = (from d in db.Payments where d.SectorId == a.SectorId && d.PaymentTypeId == 1 && d.PaymentDate >= dateFrom && d.PaymentDate <= dateTo && d.CategoryId == categoryId select d.Value).Sum(),
                    Name = a.Name,
                    CategoryName = db.PaymentCategories.Where(a=>a.Id == categoryId).FirstOrDefault().Name
                }).ToList();
            }
            return statistics;
        }
    }
}
