using FluentValidation;
using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class StudentCreateDto:Student
    {
        public List<Sector> Sectors { get; set; }
        public List<StudentClass> StudentClasses { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }
        public List<StudentType> StudentTypes { get; set; }
        public List<ContractType> ContractTypes { get; set; }
        public int ContractType { get; set; }
        public DateTime ContractDate { get; set; }
        public int Value { get; set; }
        public int Discount { get; set; }
        public int DiscountType { get; set; }
        public class StudentCreateDtoValidator : AbstractValidator<StudentCreateDto>
        {
            public StudentCreateDtoValidator()
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
                RuleFor(a => a.ContractType).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.ContractDate).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.Value).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.Discount).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.DiscountType).NotNull().WithMessage("Sahəni doldurun");
            }
        }
    }
}
