using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Security;

namespace DabisBank.Models
{
    [MetadataType(typeof(UserValidation))]
    public partial class Users
    {
        private StringBuilder query;
        public string ValuesToString()
        {
            return "'" + this.UserName + "', '" + this.Password
            + "', '" + this.Name + "', '" + this.Email + "', '" + this.Address + "', '" + 2 + "'";
        }

        public StringBuilder AddUser()
        {
            query = new StringBuilder();

            query.Append("INSERT INTO dbo.Users (UserName, Password, Name, Email, Address, RoleID) VALUES (");
            query.Append(this.ValuesToString() + ")");

            return query;
        }
    }

    public class UserValidation
    {
        public int UserID { get; set; }
        [Display(Name = "User Name")]
        [Required]
        [MinLength(4), MaxLength(50)]
        [RegularExpression(@"^[A-Za-z0-9]{4,25}$", ErrorMessage="Only Letters and numbers allowed. between 4 - 25 charecters")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,14}$", ErrorMessage="Must have UPPER case letters, lower case letters. Between 6 - 14 Charecters")]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Display(Name = "Role")]
        public int RoleID { get; set; }
    }
}