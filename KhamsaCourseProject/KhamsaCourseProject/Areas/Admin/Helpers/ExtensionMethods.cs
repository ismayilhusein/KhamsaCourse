using KhamsaCourseProject.Areas.Admin.Data;
using KhamsaCourseProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KhamsaCourseProject.Areas.Admin.Helpers
{
    public static class ExtensionMethods
    {
        public static User WithoutPassword(this User user)
        {
            if (user is null) return null;

            user.Password = string.Empty;

            return user;
        }
        public static bool IsAuthenticated(this AdminContext context, int id, string token)
        {
            User user = context.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user is object)
            {
                if (user.Token == token)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool IsHaveRole(this AdminContext context, int id, int typeId, int roleId)
        {
            User user = context.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user is null)
            {
                return false;
            }
            Role roles = context.Roles.Where(a => a.TypeId == typeId && a.ActivityId == roleId).FirstOrDefault();
            if (roles is null)
            {
                return false;
            }
            return true;
        }
    }
}
