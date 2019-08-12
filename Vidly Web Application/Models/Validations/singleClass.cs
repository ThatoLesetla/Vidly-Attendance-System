using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly_Web_Application.Models.Validations
{
    
    public class singleClass : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string classDay;

            var schedule = (Schedule)validationContext.ObjectInstance;

            if(schedule.ClassDay == null)
            {
                return new ValidationResult("Class Day is Required");

            } else {

                classDay = schedule.ClassDay;

                var day = db.Schedules.Find(classDay);

                if (day != null)
                {
                    return new ValidationResult("Lecture has class on that day");
                }
            }

            return ValidationResult.Success;
            
        }
    }
}