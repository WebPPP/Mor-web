using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Site.Models
{
    public class LoginVM
    {
        [Display(Name = "User Name")]
        [Required]
        [MinLength(4), MaxLength(50)]
        [RegularExpression(@"^[A-Za-z0-9]{4,25}$")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,12}$")]
        public string Password { get; set; }
    }
}