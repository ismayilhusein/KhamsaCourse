﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class ActivityValidator : AbstractValidator<Activity>
        {
            public ActivityValidator()
            {
                RuleFor(a => a.Id).NotNull();
                RuleFor(a => a.Name).NotNull().MaximumLength(100).WithMessage("Ad boş ola bilməz");
            }
        }
    }
}
