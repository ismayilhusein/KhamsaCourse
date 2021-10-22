using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Dtos;
using KhamsaCourseProject.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhamsaCourseProject.Areas.Admin.Filters;
using static KhamsaCourseProject.Areas.Admin.Helpers.Enums.AuthRole;

namespace KhamsaCourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(IncludeRoles))]
    public class StatisticsController : Controller
    {
        private readonly AdminContext _db;
        public StatisticsController(AdminContext db)
        {
            _db = db;
        }
        [ValidateRole(1, (int)RoleTypes.Permission)]
        public IActionResult Index([FromQuery] int id, [FromQuery] string daterange, [FromQuery] int categoryId)
        {
            var dateFrom = DateTime.Now.AddDays(-1);
            var dateTo = DateTime.Now.AddDays(1);

            if (!string.IsNullOrEmpty(daterange))
            {
                string[] dateFull = daterange.Split("-");
                dateFrom = Convert.ToDateTime(dateFull[0]);
                dateTo = Convert.ToDateTime(dateFull[1]);
            }

            ViewData["DateFrom"] = dateFrom.ToString("MM/dd/yyyy");
            ViewData["DateTo"] = dateTo.ToString("MM/dd/yyyy");

            StatisticsIndexDto model = new StatisticsIndexDto();
            
            model.Statistcs = ExConverter.Filterize(id, dateFrom, dateTo, categoryId, _db);

            model.Sectors = _db.Sectors.Where(a => a.IsActive == 1).ToList();

            model.Categories = _db.PaymentCategories.ToList();

            return View(model);
        }
    }
}
