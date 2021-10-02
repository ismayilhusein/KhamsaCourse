using FluentValidation;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class PaymentCategory:PaymentBase
    {
        public class PaymentCategoryValidator : AbstractValidator<PaymentCategory>
        {
            public PaymentCategoryValidator()
            {
                Include(new PaymentBaseValidator());
            }
        }
    }
}