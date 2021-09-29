using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class StudentBaseTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public class StudentBaseTypesValidator : AbstractValidator<StudentBaseTypes>
        {
            public StudentBaseTypesValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().MaximumLength(100).WithMessage("Ad boş ola bilməz");
            }
        }
    }
}
