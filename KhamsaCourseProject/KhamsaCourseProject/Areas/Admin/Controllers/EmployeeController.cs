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
using KhamsaCourseProject.Areas.Admin.Filters;
namespace KhamsaCourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TypeFilter(typeof(IncludeRoles))]
    public class EmployeeController : Controller
    {
        private readonly AdminContext _db;
        public EmployeeController(AdminContext db)
        {
            _db = db;
        }
        public IActionResult Index
            (
            int id,
            [FromQuery] string Fullname,
            [FromQuery] int page = 0,
            [FromQuery] int EmployeeTypes = 0,
            [FromQuery] int Status = 0
            )
        {
            EmployeeIndexDto model = new EmployeeIndexDto();

            model.EmployeeTypes = _db.EmployeeTypes.ToList();

            List<Employee> data = ExConverter.Filterize(_db, Fullname, id, EmployeeTypes, Status);

            float pagecount = data.Count;

            int count = (int)Math.Ceiling(pagecount / 10);

            data = (data).Skip(page * 10).Take(10).ToList();

            model.Employees = data;
            model.Pagination = ExConverter.PaginationMethod(page, count);

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            EmployeeCreateDto model = new EmployeeCreateDto();

            model.EmployeeTypes = _db.EmployeeTypes.ToList();
            model.Sectors = _db.Sectors.Where(a => a.IsActive == 1).ToList();
            model.ContractTypes = _db.ContractTypes.ToList();

            return View(model);
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateDto request)
        {
            #region Mapping StudentValue
            Employee employee = request.Adapt(request);

            EmployeeContract contract = new EmployeeContract
            {
                ContractDate = request.ContractDate,
                ContractTypeId = request.ContractType,
                Value = request.Value,
                Debt = 0
            };
            employee.IsActive = 1;
            #endregion

            _db.EmployeeContracts.Add(contract);

            contract.Employee = employee;

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = employee.SectorId });
        }
        public IActionResult Activate(int id)
        {
            Employee st = _db.Employees.Where(a => a.Id == id).FirstOrDefault();
            st.IsActive = 1;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = st.SectorId });
        }
        public IActionResult Deactivate(int id)
        {
            Employee st = _db.Employees.Where(a => a.Id == id).FirstOrDefault();
            st.IsActive = 0;
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = st.SectorId });
        }
        public IActionResult Delete(int id)
        {
            Employee user = _db.Employees.Where(a => a.Id == id).FirstOrDefault();
            if (user is object)
            {
                _db.Employees.Remove(user);
                _db.SaveChanges();
            }
            return RedirectToAction("Index", new { id = user.SectorId });
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            EmployeeCreateDto model = new EmployeeCreateDto();

            EmployeeContract contract = _db.EmployeeContracts.Where(a => a.EmployeeId == id).FirstOrDefault();

            Employee student = _db.Employees.Where(a => a.Id == id).FirstOrDefault();

            List<EmployeeType> employeeTypes = _db.EmployeeTypes.ToList();

            #region Additional Mapping
            model = model.ConvertType(emp:student);
            model.EmployeeTypes = employeeTypes;
            model.Sectors = _db.Sectors.Where(a => a.IsActive == 1).ToList();
            model.ContractTypes = _db.ContractTypes.ToList();
            model.ContractDate = contract.ContractDate;
            model.ContractType = contract.ContractTypeId;
            model.Value = (int)contract.Value;
            #endregion

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeCreateDto request, int id)
        {
            Employee employee = _db.Employees.Where(a => a.Id == id).FirstOrDefault();

            EmployeeContract contract = _db.EmployeeContracts.Where(a => a.EmployeeId == id).FirstOrDefault();

            #region Additional Mapping
            employee.Fullname = request.Fullname;
            employee.PhoneNumber = request.PhoneNumber;
            employee.MobileNumber = request.MobileNumber;
            employee.RegistrationDate = request.RegistrationDate;
            employee.SectorId = request.SectorId;
            employee.EmployeeTypeId = request.EmployeeTypeId;
            contract.ContractTypeId = request.ContractType;
            contract.ContractDate = request.ContractDate;
            contract.Value = request.Value;
            #endregion

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = employee.SectorId });
        }
        [HttpGet]
        public IActionResult PayContract(int id)
        {
            EmployeePayDto model = new EmployeePayDto
            {
                Contract = _db.EmployeeContracts
                .Where(a => a.EmployeeId == id)
                .Include(a => a.Employee)
                .FirstOrDefault()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult CostContract(int id)
        {
            EmployeePayDto model = new EmployeePayDto
            {
                Contract = _db.EmployeeContracts
                .Where(a => a.EmployeeId == id)
                .Include(a => a.Employee)
                .FirstOrDefault()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult PayContract(StudentPayDto payment, int id)
        {
            Employee student = _db.Employees.Where(a => a.Id == id).FirstOrDefault();

            if (student is null)
            {
                TempData["Student-Pay-Error"] = "Belə bir İşci Yoxdur";
                return RedirectToAction("Index", "Home");
            }

            EmployeeContract contract = _db.EmployeeContracts.Where(a => a.EmployeeId == id).FirstOrDefault();

            if (contract is null)
            {
                TempData["Student-Pay-Error"] = "Müqavilə tapılmadı";
                return RedirectToAction("Index", new { id = student.SectorId });
            }
            #region Mapping Specify
            contract.Debt = contract.Debt - payment.Value;

            StudentPayment studentPayment = payment.Adapt<StudentPayment>();

            studentPayment.CategoryId = 4;

            studentPayment.PaymentTypeId = 1;

            studentPayment.ProcessId = id;

            studentPayment.SectorId = student.SectorId;

            #endregion

            _db.Payments.Add(studentPayment);

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = student.SectorId });

        }
        [HttpPost]
        public IActionResult CostContract(StudentPayDto payment, int id)
        {
            Employee student = _db.Employees.Where(a => a.Id == id).FirstOrDefault();

            if (student is null)
            {
                TempData["Student-Pay-Error"] = "Belə bir İşci Yoxdur";
                return RedirectToAction("Index", "Home");
            }

            EmployeeContract contract = _db.EmployeeContracts.Where(a => a.EmployeeId == id).FirstOrDefault();

            if (contract is null)
            {
                TempData["Student-Pay-Error"] = "Müqavilə tapılmadı";
                return RedirectToAction("Index", new { id = student.SectorId });
            }
            #region Mapping Specify
            contract.Debt = contract.Debt - payment.Value;

            StudentPayment studentPayment = payment.Adapt<StudentPayment>();

            studentPayment.CategoryId = 3;

            studentPayment.PaymentTypeId = 2;

            studentPayment.ProcessId = id;

            studentPayment.SectorId = student.SectorId;

            #endregion

            _db.Payments.Add(studentPayment);

            _db.SaveChanges();

            return RedirectToAction("Index", new { id = student.SectorId });

        }
        public IActionResult Details(int id, [FromQuery] string daterange)
        {
            EmployeeContract contract = _db.EmployeeContracts.Where(a => a.EmployeeId == id).FirstOrDefault();

            var dateFrom = DateTime.Now.AddDays(-1);
            var dateTo = DateTime.Now.AddDays(1);

            if (!string.IsNullOrEmpty(daterange))
            {
                string[] dateFull = daterange.Split("-");
                dateFrom = Convert.ToDateTime(dateFull[0]);
                dateTo = Convert.ToDateTime(dateFull[1]);
            }

            object payments = _db.Payments.Where(a => a.ProcessId == id && (a.CategoryId == 3 || a.CategoryId == 4) && a.PaymentDate >= dateFrom && a.PaymentDate <= dateTo
            ).ToList().Select(a => new EmployeePaymentModalDto
            {
                PaymentDate = a.PaymentDate.ToString("yyyy-MMM-dd HH:mm"),
                Value = a.Value,
                ContractValue = contract.Value,
                PaymentType = a.PaymentTypeId
            }).ToList();

            return Json(new {
                payments = payments,
                datefrom = dateFrom.ToString("MM/dd/yyyy"),
                dateto = dateTo.ToString("MM/dd/yyyy"),
                id = id
            });

        }
    }
}
