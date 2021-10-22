using FluentValidation;
using KhamsaCourseProject.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Permission { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<Role> Roles { get; set; }
        public class UserValidator : AbstractValidator<User>
        {
            public UserValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().WithMessage("Ad boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Surname).NotNull().WithMessage("Soyad boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Permission).NotNull().WithMessage("Vəzifə boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Username).NotNull().WithMessage("İstifadəçi adı boş ola bilməz").MinimumLength(6).WithMessage("Minimum Xarakter 6").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Password).NotNull().WithMessage("Şifrə boş ola bilməz").MinimumLength(6).WithMessage("Minimum Xarakter 6");
                RuleFor(a => a.Email).NotNull().WithMessage("Email boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100").EmailAddress().WithMessage("Email formatı düzgün deyil");
                RuleFor(a => a.MobileNumber).NotNull().WithMessage("Nömrə boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
                RuleFor(a => a.Token).NotNull().WithMessage("Nömrə boş ola bilməz").MaximumLength(100).WithMessage("Max Xarakter 100");
            }
        }
    }
}
