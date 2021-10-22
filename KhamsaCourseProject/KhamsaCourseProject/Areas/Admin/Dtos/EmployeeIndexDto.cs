using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class EmployeeIndexDto
    {
        public PaginationDto Pagination { get; set; }
        public List<Employee> Employees { get; set; }
        public List<EmployeeType> EmployeeTypes { get; set; }
    }
}
