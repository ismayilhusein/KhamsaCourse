using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class Sector
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public List<Student> Students { get; set; }
        public class SectorValidator : AbstractValidator<Sector>
        {
            public SectorValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().MaximumLength(100).WithMessage("Filial adı boş ola bilməz");
                RuleFor(a => a.Phone).MaximumLength(100);
                RuleFor(a => a.Fax).MaximumLength(150);
                RuleFor(a => a.Email).MaximumLength(200);
            }
        }
    }
}
