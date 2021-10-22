using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class EmployeeContract
    {
        public int Id { get; set; }
        public DateTime ContractDate { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Value { get; set; }
        public decimal Debt { get; set; }
        public int ContractTypeId { get; set; }
        public ContractType ContractType { get; set; }
    }
}
