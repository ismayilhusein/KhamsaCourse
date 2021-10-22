using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class StatisticsIndexDto
    {
        public List<StatisticsSelectDto> Statistcs { get; set; }
        public List<PaymentCategory> Categories { get; set; }
        public List<Sector> Sectors { get; set; }
    }
}
