using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DabisBank.Models;

namespace DabisBank.Controllers
{
    public class HomeController : Controller
    {
        private MorBankEntities bankDB = new MorBankEntities();

        [SessionAuthorize, Route("")]
        public ActionResult Index()
        {
            Users user = Session["User"] as Users;
            if (bankDB.Accounts.Find(user.UserID) == null)
                ViewBag.hasBank = false;
            else
                ViewBag.hasBank = true;
            return View();
        }

        [Route("About")]
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }
        [Route("Contact")]
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }

        [Route("Error")]
        public ActionResult Error(string errorMessage)
        {
            ViewBag.error = errorMessage;
            return View();
        }
    }
}