using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace DabisBank.Models
{
    [MetadataType(typeof(RoleValidation))]
    public partial class Roles
    {
    }

    public class RoleValidation
    {
        [Display(Name = "Role")]
        public string Name { get; set; }
    }

    [MetadataType(typeof(AccountsValidation))]
    public partial class Accounts
    {
    }
    public class AccountsValidation
    {
        [Display(Name="Account Number")]
        public int AccountNumber { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name="Total Cash")]
        public double TotalCash { get; set; }
    }
}