using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Dtos
{
    public class RolesIndexDto
    {
        public List<Role> UserRoles { get; set; }
        public List<Sector> UserSectors { get; set; }
        public List<Activity> UserActivities { get; set; }
        public User User { get; set; }
        public List<int> Sectors { get; set; }
        public List<int> Activities { get; set; }
    }
}
