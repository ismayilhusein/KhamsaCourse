using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Models
{
    public class Role
    {
        public long Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ActivityId { get; set; }

        public int TypeId { get; set; }
    }
}
