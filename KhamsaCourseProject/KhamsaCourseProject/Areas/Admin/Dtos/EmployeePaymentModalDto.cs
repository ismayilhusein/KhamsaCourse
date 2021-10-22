using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class EmployeePaymentModalDto:PaymentModalDto
    {
        public int PaymentType { get; set; }
    }
}
