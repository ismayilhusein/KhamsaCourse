using FluentValidation;
using System.Collections.Generic;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class ContractType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StudentContract> Contracts { get; set; }
        public class ContractTypeValidator : AbstractValidator<ContractType>
        {
            public ContractTypeValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().MaximumLength(100).WithMessage("Ad boş ola bilməz");
            }
        }
    }
}