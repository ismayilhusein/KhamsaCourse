using FluentValidation;
namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class StudentType:StudentBaseTypes
    {
        public class StudentTypeValidator : AbstractValidator<StudentType>
        {
            public StudentTypeValidator()
            {
                Include(new StudentBaseTypesValidator());
            }
        }
    }
}