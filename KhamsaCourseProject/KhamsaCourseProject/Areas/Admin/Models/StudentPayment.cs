using FluentValidation;
using System;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class StudentPayment
    {
        public long Id { get; set; }
        public decimal Value { get; set; }
        public DateTime PaymentDate { get; set; }
        public int ProcessId { get; set; }
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
        public int CategoryId { get; set; }
        public PaymentCategory Category { get; set; }
        public string Description { get; set; }

        public class StudentPaymentValidator : AbstractValidator<StudentPayment>
        {
            public StudentPaymentValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Value).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.PaymentDate).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.PaymentTypeId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.CategoryId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.ProcessId).NotNull().WithMessage("Sahəni doldurun");
                RuleFor(a => a.Description).MaximumLength(200).WithMessage("200 xarakterdən artıq ola bilməz");
            }
        }
    }
}