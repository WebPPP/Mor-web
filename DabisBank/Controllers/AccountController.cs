using System.Web.Mvc;
using DabisBank.Models;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Net;
using System;
using System.Linq;
using System.Collections.Generic;

namespace DabisBank.Controllers
{
    public class AccountController : Controller
    {
        private sitedbEntities db = new sitedbEntities();
        private BankDB bankDB = new BankDB();

        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RegisterModal()
        {
            return PartialView();
        }

        [HttpPost, Route("Login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM loginUser)
        {
            if (ModelState.IsValid)
            {
                var userName = new SqlParameter("@userName", loginUser.UserName);
                var pass = new SqlParameter("@pass", loginUser.Password);

                Users user = (Users)db.Users.SqlQuery("SELECT * FROM dbo.Users WHERE UserName = @userName AND Password = @pass", userName, pass).FirstOrDefault<Users>();
                if (user == null)
                    return View(loginUser);
                else
                {
                    Session["User"] = user as Users;
                    Session["UserName"] = user.UserName;
                    Session["RoleID"] = user.UserID;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Database.ExecuteSqlCommand(user.AddUser().ToString());
                    //db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var exception = new FormattedDbEntityValidationException(e);
                    throw exception;
                }
                Session.Clear();
                Session["User"] = user as Users;
                Session["UserName"] = user.UserName;
                Session["RoleID"] = user.UserID;

                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpGet]
        [Route("UpdateUser")]
        [SessionAuthorize]
        public ActionResult Edit()
        {
            Users user = Session["User"] as Users;
            var id = new SqlParameter("@ID", user.UserID);

            Users editUser = (Users)db.Users.SqlQuery("SELECT * FROM dbo.Users WHERE UserID = @ID", id).FirstOrDefault<Users>();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Password,Name,Email,Address,RoleID")]Users editedUser)
        {
            if (ModelState.IsValid)
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@id", editedUser.UserID));
                param.Add(new SqlParameter("@userName", editedUser.UserName));
                param.Add(new SqlParameter("@password", editedUser.Password));
                param.Add(new SqlParameter("@name", editedUser.Name));
                param.Add(new SqlParameter("@email", editedUser.Email));
                param.Add(new SqlParameter("@address", editedUser.Address));
                param.Add(new SqlParameter("@roleId", Convert.ToInt32(Session["RoleID"])));
                object[] parameters = param.ToArray();

                try
                {
                    db.Database.ExecuteSqlCommand("UPDATE dbo.Users SET UserName = @userName, Password = @password, Name = @name,Email = @email, Address = @address, RoleID = @roleId WHERE UserID = @id", parameters);
                }
                catch
                {
                }
            }
            ViewBag.Message = "Succeed!";
            ViewBag.MessageColor = "Green";

            return View(editedUser);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}