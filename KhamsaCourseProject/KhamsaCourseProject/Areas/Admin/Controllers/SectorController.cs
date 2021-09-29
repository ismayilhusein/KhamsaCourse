using KhamsaCourseProject.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SectorController : Controller
    {
        private readonly AdminContext _context;

        public SectorController(AdminContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Sectors.Where(a=>a.IsActive == 1).ToList());
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
