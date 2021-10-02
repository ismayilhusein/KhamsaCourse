using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class PaymentType:PaymentBase
    {
        public class PaymentTypeValidator : AbstractValidator<PaymentType>
        {
            public PaymentTypeValidator()
            {
                Include(new PaymentBaseValidator());
            }
        }
    }
}
