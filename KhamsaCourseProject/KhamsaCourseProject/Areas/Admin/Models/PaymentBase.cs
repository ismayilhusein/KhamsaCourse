using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class PaymentBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StudentPayment> Payments { get; set; }
        public class PaymentBaseValidator : AbstractValidator<PaymentBase>
        {
            public PaymentBaseValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().MaximumLength(100).WithMessage("Ad boş ola bilməz");
            }
        }
    }
}
