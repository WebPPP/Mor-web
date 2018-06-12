using System.Web.Mvc;
using DabisBank.Models;

namespace DabisBank
{

    public class SessionAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Session["User"] as Users == null)
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }

    public class AdminAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            Users user = context.HttpContext.Session["User"] as Users;
            if (user == null || user.RoleID != 1)
            {
                context.Result = new RedirectResult("");
            }
        }
    }

    public class BankAuthorize : AuthorizeAttribute
    {
        private BankDB bank = new BankDB();
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            Users user = context.HttpContext.Session["User"] as Users;
            if (bank.Accounts.Find(user.UserID) == null)
                context.Result = new RedirectResult("");
        }
    }
}