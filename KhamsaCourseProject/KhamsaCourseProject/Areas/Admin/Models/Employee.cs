using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public int IsActive { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<StudentPayment> StudentPayments { get; set; }
        public int SectorId { get; set; }
        public Sector Sector { get; set; }
        public int EmployeeTypeId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int EmployeeContractId { get; set; }
        public EmployeeContract EmployeeContract { get; set; }
        public class EmployeeValidator : AbstractValidator<Employee>
        {
            public EmployeeValidator()
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
