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
           .Include(a=>a.Contract)
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

    }
}
