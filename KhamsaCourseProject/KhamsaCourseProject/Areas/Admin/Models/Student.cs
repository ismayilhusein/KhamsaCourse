using FluentValidation;
using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public int StudentTypeId { get; set; }
        public StudentType StudentType { get; set; }
        public int StudentClassId { get; set; }
        public StudentClass StudentClass { get; set; }
        public int StudentGroupId { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public string HomeNumber { get; set; }
        public string MobileNumber { get; set; }
        public int IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<StudentPayment> StudentPayments { get; set; }
        public Sector Sector { get; set; }
        public int SectorId { get; set; }
        public class StudentValidator : AbstractValidator<Student>
        {
            public StudentValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.StudentTypeId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.SectorId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.StudentClassId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.StudentGroupId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.RegistrationDate).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.Fullname).NotNull().WithMessage("Sahəni doldurun").MaximumLength(300).WithMessage("Şrift sayı 300 keçməməlidir");
                RuleFor(a => a.HomeNumber).NotNull().WithMessage("Sahəni doldurun").MaximumLength(100).WithMessage("Şrift sayı 100 keçməməlidir");
                RuleFor(a => a.MobileNumber).NotNull().WithMessage("Sahəni doldurun").MaximumLength(100).WithMessage("Şrift sayı 100 keçməməlidir");
            }
        }


    }
}
