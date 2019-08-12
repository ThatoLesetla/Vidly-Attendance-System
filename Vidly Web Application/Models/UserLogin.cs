using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models.Validations;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
    }

    public class UserMetaData
    {
        [Required]
        [StringLength(40)]
        [EmailAddress]
        public string UserEmail { get; set; }
        public string Username { get; set; }
        [Required]
        [StringLength(55)]
        public string UserPassword { get; set; }
        public bool UserStatus { get; set; }
        public string UserRole { get; set; }
        public string UserIP { get; set; }
        public string OTP { get; set; }
  
    }
}