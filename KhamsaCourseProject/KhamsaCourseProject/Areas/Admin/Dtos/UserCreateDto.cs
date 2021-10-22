using FluentValidation;
using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class UserCreateDto:User
    {
        public List<int> Sectors { get; set; }
        public List<int> Activities { get; set; }
        public List<Sector> SectorUser { get; set; }
        public List<Activity> ActivityUser { get; set; }
        public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
        {
            public UserCreateDtoValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().WithMessage("Ad boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Surname).NotNull().WithMessage("Soyad boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Permission).NotNull().WithMessage("Vəzifə boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Username).NotNull().WithMessage("İstifadəçi adı boş ola bilməz").MinimumLength(6).WithMessage("Minimum Xarakter 6").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Password).NotNull().WithMessage("Şifrə boş ola bilməz").MinimumLength(6).WithMessage("Minimum Xarakter 6");
                RuleFor(a => a.Email).NotNull().WithMessage("Email boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100").EmailAddress().WithMessage("Email formatı düzgün deyil");
                RuleFor(a => a.MobileNumber).NotNull().WithMessage("Nömrə boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
            }
        }
    }
}
