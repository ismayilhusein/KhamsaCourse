using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class StudentContract
    {
        public int Id { get; set; }
        public DateTime ContractDate { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public decimal Value { get; set; }
        public decimal Debt { get; set; }
        public decimal Discount { get; set; }
        public int ContractTypeId { get; set; }
        public ContractType ContractType { get; set; }
    }
}
