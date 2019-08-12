using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models.Validations
{
    public class timeValidation : ValidationAttribute
    {

        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TimeSpan startTime, endTime;
            var schedule = (Schedule)validationContext.ObjectInstance;

            startTime = (TimeSpan)schedule.StartTime;
            endTime = (TimeSpan)schedule.EndTime;

           
                if (startTime < endTime)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("StartTime must be less then endTime");
                }
           
        }
    }
}