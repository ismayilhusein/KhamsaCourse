using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Helpers;
using KhamsaCourseProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KhamsaCourseProject.Areas.Admin.Helpers.Enums.AuthRole;

namespace KhamsaCourseProject.Areas.Admin.Filters
{
    public class ValidateRole : Attribute, IAsyncActionFilter
    {
        private readonly int rid;
        private readonly int typeid;
        public ValidateRole(int rid, int typeid)
        {
            this.rid = rid;
            this.typeid = typeid;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Controller controller = context.Controller as Controller;

            if (controller != null)
            {
                if (controller.ViewData["User-Roles"] is object)
                {
                    List<Role> roles = controller.ViewData["User-Roles"] as List<Role>;
                    int id = Convert.ToInt32(context.HttpContext.Request.Cookies["uid"]);
                    Role role = roles.Where(a=>a.ActivityId == rid && a.TypeId == typeid).FirstOrDefault();
                    if (role is object)
                    {
                        await next();
                    }
                    context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                            { "controller", "User" },
                            { "action", "Logout" }
                    });

                }
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                        { "controller", "User" },
                        { "action", "Logout" }
                });

            }
            context.Result = new RedirectToRouteResult(
            new RouteValueDictionary
            {
                    { "controller", "User" },
                    { "action", "Logout" }
            });
        }
    }
}
