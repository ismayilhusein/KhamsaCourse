using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhamsaCourseProject.Areas.Admin.Filters;
namespace KhamsaCourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(IncludeRoles))]
    public class PublicationController : Controller
    {
        private readonly AdminContext _db;
        public PublicationController(AdminContext db)
        {
            _db = db;
        }
        public IActionResult Index(int id, [FromQuery] string daterange)
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

            return View(_db.Publications.Where(a => a.SectorId == id && a.PublicationDate >= dateFrom && a.PublicationDate <= dateTo).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(int id, Publication request)
        {
            Publication exam = request;
            exam.Id = 0;
            exam.SectorId = id;
            _db.Publications.Add(exam);
            _db.SaveChanges();
            _db.Payments.Add(new StudentPayment
            {
                CategoryId = 8,
                Description = exam.Description,
                PaymentDate = exam.PublicationDate,
                PaymentTypeId = 2,
                ProcessId = exam.Id,
                Value = exam.Benefit,
                SectorId = id
            });
            _db.SaveChanges();
            return RedirectToAction("Index", new { Id = id });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Publication debt = new Publication();
            if (debt is object)
            {
                debt = _db.Publications.Where(a => a.Id == id).FirstOrDefault();
            }
            return View(debt);
        }
        [HttpPost]
        public IActionResult Edit(Publication request, int id)
        {
            Publication debt = _db.Publications.Where(a => a.Id == id).FirstOrDefault();
            if (debt is object)
            {
                debt.Name = request.Name;
                debt.Benefit= request.Benefit;
                debt.PublicationDate = request.PublicationDate;
                debt.Description = request.Description;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = debt.
                SectorId });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Publication debt = _db.Publications.Where(a => a.Id == id).FirstOrDefault();
            if (debt is object)
            {
                _db.Publications.Remove(debt);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = debt.SectorId });
        }
    }
}
