using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models.Validations
{
    public class LocationValidator : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var location = (Location)validationContext.ObjectInstance;

            var loc = db.Locations.Find(location);

            if(loc == null)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("RoomType already exits");
            }
        }
    }
}