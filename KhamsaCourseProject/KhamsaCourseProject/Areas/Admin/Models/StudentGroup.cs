using FluentValidation;
namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class StudentGroup:StudentBaseTypes
    {
        public class StudentGroupValidator : AbstractValidator<StudentClass>
        {
            public StudentGroupValidator()
            {
                Include(new StudentBaseTypesValidator());
            }
        }
    }
}