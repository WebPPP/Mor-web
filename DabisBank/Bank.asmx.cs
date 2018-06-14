using DabisBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace DabisBank
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class Bank1 : System.Web.Services.WebService
    {
        private MorBankEntities bankDB = new MorBankEntities();

        [WebMethod]
        public bool hasBankAccount(int id)
        {
            if (bankDB.Accounts.Find(id) != null)
                return true;
            else
                return false;
        }

        [WebMethod]
        public bool CreateBankAccount(int id)
        {
            if (hasBankAccount(id) == false)
            {
                Account bankAccount = new Account();
                bankAccount.UserID = id;
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
                if (hasBankAccount(id))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }


        [WebMethod]
        public double CashInAccount(int id)
        {
            if (hasBankAccount(id))
            {
                return bankDB.Accounts.Find(id).TotalCash;
            }
            else
                return 0;
        }

        [WebMethod]
        public bool ChargeAccount(int id, double CashToCharge)
        {
            if (hasBankAccount(id))
            {
                double money = bankDB.Accounts.Find(id).TotalCash;

                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@id", id));
                param.Add(new SqlParameter("@cash", (money - CashToCharge)));
                object[] parameters = param.ToArray();

                bankDB.Database.ExecuteSqlCommand("UPDATE dbo.Accounts SET TotalCash = @cash WHERE UserID = @id", parameters);

                if (bankDB.Accounts.Find(id).TotalCash == (money + CashToCharge))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}
