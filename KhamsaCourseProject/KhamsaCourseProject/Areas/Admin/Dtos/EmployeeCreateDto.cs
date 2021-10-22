using FluentValidation;
using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class EmployeeCreateDto:Employee
    {
        public List<Sector> Sectors { get; set; }
        public List<EmployeeType> EmployeeTypes { get; set; }
        public List<ContractType> ContractTypes { get; set; }
        public int ContractType { get; set; }
        public DateTime ContractDate { get; set; }
        public int Value { get; set; }
        public class EmployeeCreateDtoValidator : AbstractValidator<EmployeeCreateDto>
        {
            public EmployeeCreateDtoValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.EmployeeTypeId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.SectorId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.RegistrationDate).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.Fullname).NotNull().WithMessage("Sahəni doldurun").MaximumLength(300).WithMessage("Şrift sayı 300 keçməməlidir");
                RuleFor(a => a.PhoneNumber).NotNull().WithMessage("Sahəni doldurun").MaximumLength(100).WithMessage("Şrift sayı 100 keçməməlidir");
                RuleFor(a => a.MobileNumber).NotNull().WithMessage("Sahəni doldurun").MaximumLength(100).WithMessage("Şrift sayı 100 keçməməlidir");
            }
        }

    }
}
