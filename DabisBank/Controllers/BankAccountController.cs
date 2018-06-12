using System.Web.Mvc;
using DabisBank.Models;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DabisBank.Controllers
{
    [SessionAuthorize]
    public class BankAccountController : Controller
    {
        private BankDB bankDB = new BankDB();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("CreateBankAccount")]
        public ActionResult Create()
        {
            Users user = Session["User"] as Users;
            Accounts bankAccount = new Accounts();
            bankAccount.UserID = user.UserID;
            bankAccount.TotalCash = 1000000f;

            try
            {
                bankDB.Accounts.Add(bankAccount);
                bankDB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var exception = new FormattedDbEntityValidationException(e);
                throw exception;
            }

            return RedirectToAction("Index", "Home");
        }

        [BankAuthorize]
        [HttpGet, Route("AccountDetails")]
        public ActionResult Details()
        {
            Users user = Session["User"] as Users;
            ViewBag.userName = user.UserName;
            Accounts bankAccount = bankDB.Accounts.Find(user.UserID);

            return View(bankAccount);
        }

        [HttpGet, Route("Withdrawal")]
        public ActionResult Withdrawal()
        {
            return PartialView();
        }

        [HttpPost, Route("Withdrawal")]
        public ActionResult Withdrawal(WithdrawalModel model)
        {
            Users user = Session["User"] as Users;
            Accounts bankAccount = bankDB.Accounts.Find(user.UserID);

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@id", user.UserID));
            param.Add(new SqlParameter("@cash", (bankAccount.TotalCash - model.AmountToWithdrawal)));
            object[] parameters = param.ToArray();

            bankDB.Database.ExecuteSqlCommand("UPDATE dbo.Accounts SET TotalCash = @cash WHERE UserID = @id", parameters);

            return RedirectToAction("Index", "Home");
        }
    }
}