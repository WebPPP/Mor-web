using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DabisBank.Models;
using System.Data.SqlClient;
using System.Data.Entity.Validation;

namespace DabisBank.Controllers
{
    [AdminAuthorize]
    public class BankManagerController : Controller
    {
        private MorBankEntities bankDB = new MorBankEntities();

        [Route("Accounts")]
        public ActionResult Index()
        {
            var accounts = bankDB.Accounts;
            return View(accounts.ToList());
        }

        [Route("AccountDetailsM")]
        public ActionResult Details(int id)
        {
            Accounts bankAccount =new Accounts(bankDB.Accounts.FirstOrDefault(a => a.UserID == id));
            return View(bankAccount);
        }

        [HttpGet]
        public ActionResult GiveCash(int id)
        {
            Accounts bankAccount = new Accounts(bankDB.Accounts.FirstOrDefault(a=>a.UserID == id));
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("@id", id));
            param.Add(new SqlParameter("@cash", (bankAccount.TotalCash + 150000)));
            object[] parameters = param.ToArray();

            bankDB.Database.ExecuteSqlCommand("UPDATE dbo.Accounts SET TotalCash = @cash WHERE UserID = @id", parameters);
            return RedirectToAction("Index");
        }

        [HttpGet, Route("DeleteAccount")]
        public ActionResult Delete(int id)
        {
            Accounts bankAccount = new Accounts(bankDB.Accounts.Find(id));
            return View(bankAccount);
        }
        
        [HttpPost, Route("DeleteAccount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account bankAccount = bankDB.Accounts.Find(id);

            try
            {
                bankDB.Accounts.Remove(bankAccount);
                bankDB.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var exception = new FormattedDbEntityValidationException(e);
                throw exception;
            }

            return RedirectToAction("Index");
        }
    }
}