using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models.Validations
{
    public class userValidation : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            var userInfo = db.Locations.Find(user.UserEmail);

            if (userInfo == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("User already exits");
            }
        }
    }
}