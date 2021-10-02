using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class StudentIndexDto
    {
        public List<Student> Students { get; set; }
        public PaginationDto Pagination { get; set; }
        public List<StudentClass> StudentClasses { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }
        public List<StudentType> StudentTypes { get; set; }
    }
}
