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
    public class ExtraPaymentController : Controller
    {
        private readonly AdminContext _db;
        public ExtraPaymentController(AdminContext db)
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

            return View(_db.ExtraPayments.Where(a => a.SectorId == id && a.BenefitDate >= dateFrom && a.BenefitDate <= dateTo).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(int id, ExtraPayment request)
        {
            ExtraPayment exam = request;
            exam.Id = 0;
            exam.SectorId = id;
            _db.ExtraPayments.Add(exam);
            _db.SaveChanges();
            _db.Payments.Add(new StudentPayment
            {
                CategoryId = 7,
                Description = exam.Description,
                PaymentDate = exam.BenefitDate,
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
            ExtraPayment debt = new ExtraPayment();
            if (debt is object)
            {
                debt = _db.ExtraPayments.Where(a => a.Id == id).FirstOrDefault();
            }
            return View(debt);
        }
        [HttpPost]
        public IActionResult Edit(ExtraPayment request, int id)
        {
            ExtraPayment debt = _db.ExtraPayments.Where(a => a.Id == id).FirstOrDefault();
            if (debt is object)
            {
                debt.Name = request.Name;
                debt.Benefit = request.Benefit;
                debt.BenefitDate = request.BenefitDate;
                debt.Description = request.Description;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = debt.SectorId });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ExtraPayment debt = _db.ExtraPayments.Where(a => a.Id == id).FirstOrDefault();
            if (debt is object)
            {
                _db.ExtraPayments.Remove(debt);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = debt.SectorId });
        }
    }
}
