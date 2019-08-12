using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models
{
    public class scheduleValidator : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
          
            var schedule = (Schedule)validationContext.ObjectInstance;

            Schedule shift = db.Schedules.Where(a => a.StaffID == schedule.StaffID && a.ClassDay == schedule.ClassDay && a.StartTime == schedule.StartTime).SingleOrDefault();

            if(shift == null)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("Lecturer already has class");
                
            }
           
                
        }
    }
}