using FluentValidation;
using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class StudentPayDto
    {
        public decimal Value { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public StudentContract Contract { get; set; }
        public class StudentPayDtoValidator : AbstractValidator<StudentPayDto>
        {
            public StudentPayDtoValidator()
            {
                RuleFor(a => a.Value).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.PaymentDate).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.Description).MaximumLength(200).WithMessage("200 xarakterdən artıq ola bilməz");
            }
        }
    }
}
