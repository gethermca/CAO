using System;
using System.Web;
using System.Web.Security;
using CaseManagement.Models;

namespace CaseManagement.Helpers
{
    public class SecurityHelper
    {
        public static void SetAuthenticationCookie(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Username, false);
            HttpContext.Current.Session["UserId"] = user.UserId;
            HttpContext.Current.Session["Username"] = user.Username;
            HttpContext.Current.Session["Role"] = user.Role;
            HttpContext.Current.Session["FullName"] = user.FullName;
        }

        public static void ClearAuthentication()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }

        public static int GetCurrentUserId()
        {
            return HttpContext.Current.Session["UserId"] != null ? 
                Convert.ToInt32(HttpContext.Current.Session["UserId"]) : 0;
        }

        public static string GetCurrentUserRole()
        {
            return HttpContext.Current.Session["Role"]?.ToString() ?? "Guest";
        }

        public static string GetCurrentUsername()
        {
            return HttpContext.Current.Session["Username"]?.ToString() ?? "";
        }

        public static bool IsAuthorized(string requiredRole)
        {
            string userRole = GetCurrentUserRole();
            return userRole == requiredRole || userRole == "Admin";
        }
    }
}
