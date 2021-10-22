using FluentValidation;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class EmployeeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class EmployeeTypeValidator : AbstractValidator<EmployeeType>
        {
            public EmployeeTypeValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().MaximumLength(100).WithMessage("Ad boş ola bilməz");
            }
        }
    }
}