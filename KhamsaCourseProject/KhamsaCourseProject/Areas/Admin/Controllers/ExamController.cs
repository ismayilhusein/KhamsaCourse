﻿using KhamsaCourseProject.Areas.Admin.Data;
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
    public class ExamController : Controller
    {
        private readonly AdminContext _db;
        public ExamController(AdminContext db)
        {
            _db = db;
        }
        public IActionResult Index(int id, [FromQuery]string daterange)
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

            return View(_db.Exams.Where(a=>a.SectorId == id && a.ExamDate >=dateFrom && a.ExamDate <= dateTo).ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(int id, Exam request)
        {
            Exam exam = request;
            exam.Id = 0;
            exam.SectorId = id;
            _db.Exams.Add(exam);
            _db.SaveChanges();
            _db.Payments.Add(new StudentPayment
            {
                CategoryId = 9,
                Description = exam.Description,
                PaymentDate = exam.ExamDate,
                PaymentTypeId = 2,
                ProcessId = exam.Id,
                Value = exam.Benefit,
                SectorId = id
            });
            _db.SaveChanges();
            return RedirectToAction("Index", new { Id = id});
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Exam debt = new Exam();
            if (debt is object)
            {
                debt = _db.Exams.Where(a => a.Id == id).FirstOrDefault();
            }
            return View(debt);
        }
        [HttpPost]
        public IActionResult Edit(Exam request, int id)
        {
            Exam debt = _db.Exams.Where(a => a.Id == id).FirstOrDefault();
            if (debt is object)
            {
                debt.Name = request.Name;
                debt.Benefit = request.Benefit;
                debt.ExamDate = request.ExamDate;
                debt.Description = request.Description;
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = debt.SectorId });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Exam debt = _db.Exams.Where(a => a.Id == id).FirstOrDefault();
            if (debt is object)
            {
                _db.Exams.Remove(debt);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = debt.SectorId });
        }
    }
}
