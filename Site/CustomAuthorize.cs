using System.Web.Mvc;
using Site.Models;

namespace Site
{
    public class SessionAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["User"] as User == null)
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }

    public class AdminAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            User user = context.HttpContext.Session["User"] as User;
            if (user == null || user.RoleID != 1)
            {
                context.Result = new RedirectResult("/Shop");
            }
        }
    }
}