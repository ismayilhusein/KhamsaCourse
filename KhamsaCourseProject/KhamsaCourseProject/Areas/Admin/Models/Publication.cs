using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class Publication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Benefit { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public int SectorId { get; set; }
        public Sector Sector { get; set; }
        public class PublicationValidator : AbstractValidator<Publication>
        {
            public PublicationValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().WithMessage("Material adı boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Benefit).NotNull().WithMessage("Gəlir məcburidir");
                RuleFor(a => a.PublicationDate).NotNull().WithMessage("Tarix məcburidir"); ;
                RuleFor(a => a.Description).MaximumLength(200).WithMessage("Max Xarakter 200");
            }
        }
    }
}
