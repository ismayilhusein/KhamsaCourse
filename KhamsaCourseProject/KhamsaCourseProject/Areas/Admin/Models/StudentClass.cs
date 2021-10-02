using FluentValidation;
namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class StudentClass :StudentBaseTypes
    {
        public class StudentClassValidator : AbstractValidator<StudentClass>
        {
            public StudentClassValidator()
            {
                Include(new StudentBaseTypesValidator());
            }
        }
    }
}