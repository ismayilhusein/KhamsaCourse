using CryptoHelper;
using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Dtos;
using KhamsaCourseProject.Areas.Admin.Filters;
using KhamsaCourseProject.Areas.Admin.Helpers;
using KhamsaCourseProject.Areas.Admin.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AdminContext _db;
        public UserController(AdminContext db)
        {
            _db = db;
        }
        [TypeFilter(typeof(IncludeRoles))]
        public IActionResult Index()
        {
            return View(_db.Users.Select(a => a.WithoutPassword()).ToList());
        }
        [TypeFilter(typeof(IncludeRoles))]
        [HttpGet]
        public IActionResult Create()
        {
            UserCreateDto model = new UserCreateDto();
            model.SectorUser = _db.Sectors.ToList();
            model.ActivityUser = _db.Activities.ToList();
            return View(model);
        }
        [TypeFilter(typeof(IncludeRoles))]
        [HttpPost]
        public IActionResult Create(UserCreateDto request)
        {
            User user = request.Adapt<User>();

            bool existed = _db.Users.Any(a => a.Username == request.Username);
            if (existed)
            {
                TempData["User-Error"] = "İstifadəçi adı mövcuddurç başqa ad sınayın";
                return RedirectToAction("Index");
            }
            List<Role> roles = new List<Role>();

            user.Password = Crypto.HashPassword(user.Password);

            _db.Users.Add(user);

            _db.SaveChanges();

            if (request.Sectors is object)
            {
                foreach (var item in request.Sectors)
                {
                    roles.Add(new Role { ActivityId = item, TypeId = 1, UserId = user.Id });
                }
            }
            if (request.Activities is object)
            {
                foreach (var item in request.Activities)
                {
                    roles.Add(new Role { ActivityId = item, TypeId = 2, UserId = user.Id });
                }
            }

            _db.Roles.AddRange(roles);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [TypeFilter(typeof(IncludeRoles))]
        public IActionResult Delete(int id)
        {
            User user = _db.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user is object)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [TypeFilter(typeof(IncludeRoles))]
        [HttpGet]
        public IActionResult Roles(int id)
        {
            RolesIndexDto model = new RolesIndexDto();
            model.UserRoles = _db.Roles.Where(a => a.UserId == id).ToList();
            model.UserActivities = _db.Activities.ToList();
            model.UserSectors = _db.Sectors.Where(a => a.IsActive == 1).ToList();
            model.User = _db.Users.Where(a => a.Id == id).FirstOrDefault();
            return View(model);
        }
        [TypeFilter(typeof(IncludeRoles))]
        [HttpPost]
        public IActionResult Roles(int id, RolesIndexDto request)
        {
            List<Role> roles = new List<Role>();

            List<Role> oldroles = _db.Roles.Where(a => a.UserId == id).ToList();

            if (request.Sectors is object)
            {
                foreach (var item in request.Sectors)
                {
                    roles.Add(new Role { ActivityId = item, TypeId = 1, UserId = id });
                }
            }
            if (request.Activities is object)
            {
                foreach (var item in request.Activities)
                {
                    roles.Add(new Role { ActivityId = item, TypeId = 2, UserId = id });
                }
            }
            if (oldroles.Count > 0 || roles.Count > 0)
            {
                _db.Roles.RemoveRange(oldroles);
                _db.SaveChanges();
            }
            if (roles.Count > 0)
            {
                _db.Roles.AddRange(roles);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto request)
        {
            User user = _db.Users.FirstOrDefault(a => a.Username == request.Username);
            if (user is null)
            {
                TempData["UserError"] = "Belə istifadəçi mövcud deyil";
                return RedirectToAction("Login");
            }

            if (!Crypto.VerifyHashedPassword(user.Password, request.Password))
            {
                TempData["UserError"] = "Şifrə və ya istifadəçi adı səhvdir";
                return RedirectToAction("Login");
            }

            string token = Guid.NewGuid().ToString();

            user.Token = token;

            if (request.Rememberme)
            {
                HttpContext.Response.Cookies.Append("token", token, new CookieOptions { Secure = true, HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddDays(100) });
                HttpContext.Response.Cookies.Append("uid", Convert.ToString(user.Id), new CookieOptions { Secure = true, HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddDays(100) });
            }
            else
            {
                HttpContext.Response.Cookies.Append("token", token, new CookieOptions { Secure = true, HttpOnly = true });
                HttpContext.Response.Cookies.Append("uid", Convert.ToString(user.Id), new CookieOptions { Secure = true, HttpOnly = true });
            }

            _db.SaveChanges();

            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies["token"] is object)
            {
                HttpContext.Response.Cookies.Delete("token");
                HttpContext.Response.Cookies.Delete("uid");
            }
            return RedirectToAction("Login", "User");
        }

    }
}
