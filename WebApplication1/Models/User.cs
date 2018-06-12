using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class User
    {
        [BsonId]
        public ObjectId id { get; set; }
        [Required(ErrorMessage = "Please enter the user's full name.")]
        [StringLength(50, ErrorMessage = "The First Name must be less than {1} characters.")]
        [Display(Name = "User Name")]
        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("gender")]
        public bool gender { get; set; }

        [EmailAddress(ErrorMessage = "The Email Address is not valid")]
        [Required(ErrorMessage = "please enter email address.")]
        [Display(Name = "Email")]
        [BsonElement("Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the user's Password.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The password must be less than - {1} characters , and more than - {2} characters ")]
        [Display(Name = "Password")]
        [BsonElement("password")]
        public string password { get; set; }


    }
}