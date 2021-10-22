using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Rememberme { get; set; }
        public class LoginDtoValidator : AbstractValidator<LoginDto>
        {
            public LoginDtoValidator()
            {
                RuleFor(a => a.Username).NotNull().WithMessage("İstifadəçi adı boş ola bilməz").MinimumLength(6).WithMessage("Minimum Xarakter 6").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Password).NotNull().WithMessage("Şifrə boş ola bilməz").MinimumLength(6).WithMessage("Minimum Xarakter 6");
            }
        }
    }
}
