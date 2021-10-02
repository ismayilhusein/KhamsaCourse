using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class PaymentModalDto
    {
        public string PaymentDate { get; set; }
        public decimal Value { get; set; }
        public decimal ContractValue { get; set; }
    }
}
