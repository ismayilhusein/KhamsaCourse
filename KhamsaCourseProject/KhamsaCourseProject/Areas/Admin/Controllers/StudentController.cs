using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Dtos;
using KhamsaCourseProject.Areas.Admin.Helpers;
using KhamsaCourseProject.Areas.Admin.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        private readonly AdminContext _db;
        public StudentController(AdminContext db)
        {
            _db = db;
        }
        public IActionResult Index
            (
            int id,
            [FromQuery] string Fullname,
            [FromQuery] int page = 0,
            [FromQuery] int StudentClasses = 0,
            [FromQuery] int StudentGroups = 0,
            [FromQuery] int StudentTypes = 0,
            [FromQuery] int Status = 0
            )
        {
            StudentIndexDto model = new StudentIndexDto();

            model.StudentGroups = _db.StudentGroups.ToList();
            model.StudentClasses = _db.StudentClasses.ToList();
            model.StudentTypes = _db.StudentTypes.ToList();

            List<Student> data = ExConverter.Filterize(_db, Fullname, id ,StudentClasses, StudentGroups, StudentTypes, Status);

            float pagecount = data.Count;

            int count = (int)Math.Ceiling(pagecount / 10);

            data = (data).Skip(page * 10).Take(10).ToList();

            model.Students = data;
            model.Pagination = ExConverter.PaginationMethod(page, count);

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            StudentCreateDto model = new StudentCreateDto();

            StudentContract contract = _db.Contracts.Where(a => a.StudentId == id).FirstOrDefault();

            Student student = _db.Students.Where(a => a.Id == id).FirstOrDefault();

            #region Additional Mapping
            model.Id = id;
            model.Fullname = student.Fullname;
            model.HomeNumber = student.HomeNumber;
            model.MobileNumber = student.MobileNumber;
            model.RegistrationDate = student.RegistrationDate;
            model.SectorId = student.SectorId;
            model.StudentClassId = student.StudentClassId;
            model.StudentGroupId = student.StudentGroupId;
            model.StudentTypeId = student.StudentTypeId;
            model.ContractType = contract.ContractTypeId;
            model.ContractDate = contract.ContractDate;
            model.ContractId = contract.Id;
            model.Discount = (int)contract.Discount;
            model.Value = (int)contract.Value;
            model.StudentGroups = _db.StudentGroups.ToList();
            model.StudentClasses = _db.StudentClasses.ToList();
            model.StudentTypes = _db.StudentTypes.ToList();
            model.Sectors = _db.Sectors.Where(a => a.IsActive == 1).ToList();
            model.ContractTypes = _db.ContractTypes.ToList();
            model.Contract = contract;
            #endregion

            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = _db.Students.Where(a => a.Id == id).FirstOrDefault();

            _db.Students.Remove(student);

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = student.SectorId }); 
        }
        [HttpPost]
        public IActionResult Edit(StudentCreateDto request, int id)
        {
            Student student = _db.Students.Where(a => a.Id == id).FirstOrDefault();

            StudentContract contract = _db.Contracts.Where(a => a.StudentId == id).FirstOrDefault();

            #region Additional Mapping
            student.Fullname = request.Fullname;
            student.HomeNumber = request.HomeNumber;
            student.MobileNumber = request.MobileNumber;
            student.RegistrationDate = request.RegistrationDate;
            student.SectorId = request.SectorId;
            student.StudentClassId = request.StudentClassId;
            student.StudentGroupId = request.StudentGroupId;
            student.StudentTypeId = request.StudentTypeId;
            contract.ContractTypeId = request.ContractType;
            contract.ContractDate = request.ContractDate;
            contract.Value = request.Value;
            contract.Discount = request.Discount;
            #endregion

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = student.SectorId });
        }
        public IActionResult Activate(int id)
        {
            Student st =_db.Students.Where(a => a.Id == id).FirstOrDefault();
            st.IsActive = 1;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = st.SectorId });
        }
        public IActionResult Deactivate(int id)
        {
            Student st = _db.Students.Where(a => a.Id == id).FirstOrDefault();
            st.IsActive = 0;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = st.SectorId });
        }

        [HttpGet]
        public IActionResult Create()
        {
            StudentCreateDto model = new StudentCreateDto();

            model.StudentGroups = _db.StudentGroups.ToList();
            model.StudentClasses = _db.StudentClasses.ToList();
            model.StudentTypes = _db.StudentTypes.ToList();
            model.Sectors = _db.Sectors.Where(a => a.IsActive == 1).ToList();
            model.ContractTypes = _db.ContractTypes.ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(StudentCreateDto request)
        {
            #region Mapping StudentValue
            Student student = request.Adapt(request);

            decimal discount = (decimal)ExConverter.DiscountedValue(request.DiscountType, request.Value, request.Discount);
            StudentContract contract = new StudentContract
            {
                ContractDate = request.ContractDate,
                ContractTypeId = request.ContractType,
                Discount = request.Value - discount,
                Value = discount,
                Debt = discount
            };
            student.IsActive = 1;
            #endregion

            _db.Contracts.Add(contract);

            contract.Student = student;

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = student.SectorId });
        }
        public IActionResult Details(int id)
        {
            StudentContract contract = _db.Contracts.Where(a => a.StudentId == id).FirstOrDefault();

            object payments = _db.Payments.Where(a => a.ProcessId == id && a.PaymentTypeId == 2 && a.CategoryId == 5).ToList().Select(a => new PaymentModalDto
            {
                PaymentDate = a.PaymentDate.ToString("yyyy-MMM-dd HH:mm"),
                Value = a.Value,
                ContractValue = contract.Value
            }).ToList();

            return View("PayContractModal", payments);

        }
        [HttpGet]
        public IActionResult PayContract(int id)
        {
            StudentPayDto model = new StudentPayDto
            {
                Contract = _db.Contracts
                .Where(a => a.StudentId == id)
                .Include(a => a.Student)
                .FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PayContract(StudentPayDto payment, int id)
        {
            Student student = _db.Students.Where(a => a.Id == id).FirstOrDefault();

            if (student is null)
            {
                TempData["Student-Pay-Error"] = "Belə bir Tələbə Yoxdur";
                return RedirectToAction("Index", "Home");
            }

            StudentContract contract = _db.Contracts.Where(a => a.StudentId == id).FirstOrDefault();

            if (contract is null)
            {
                TempData["Student-Pay-Error"] = "Müqavilə tapılmadı";
                return RedirectToAction("Index", new { id = student.SectorId });
            }
            #region Mapping Specify
            contract.Debt = contract.Debt - payment.Value;

            StudentPayment studentPayment = payment.Adapt<StudentPayment>();

            studentPayment.CategoryId = 5;

            studentPayment.PaymentTypeId = 2;

            studentPayment.ProcessId = id;

            studentPayment.SectorId = student.SectorId;

            #endregion

            _db.Payments.Add(studentPayment);

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = student.SectorId });

        }
    }
}
