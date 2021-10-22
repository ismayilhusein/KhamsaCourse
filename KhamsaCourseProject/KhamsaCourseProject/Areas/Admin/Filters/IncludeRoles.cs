using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace KhamsaCourseProject.Areas.Admin.Filters
{
    public class IncludeRoles : Attribute, IAsyncActionFilter
    {
        private readonly AdminContext _db;
        public IncludeRoles(AdminContext db)
        {
            _db = db;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Cookies["token"] is object)
            {
                int id = Convert.ToInt32(context.HttpContext.Request.Cookies["uid"]);
                if (_db.IsAuthenticated(id, context.HttpContext.Request.Cookies["token"]))
                {
                    Controller controller = context.Controller as Controller;

                    if (controller != null)
                    {
                        controller.ViewData["User-Roles"] = await _db.Roles.Where(a => a.UserId == id).ToListAsync();
                        controller.ViewData["User-Data"] = await _db.Users.Where(a => a.Id == id).FirstOrDefaultAsync();
                    }

                    await next();
                }
                else
                {
                    context.Result = new RedirectToRouteResult(
                      new RouteValueDictionary
                      {
                                        { "controller", "User" },
                                        { "action", "Logout" }
                      });
                }
            }
            else
            {
                context.Result = new RedirectToRouteResult(
                  new RouteValueDictionary
                  {
                                    { "controller", "User" },
                                    { "action", "Logout" }
                  });
            }

        }
    }
}
