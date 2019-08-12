using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models
{
    public class TimeValidator : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            

            var schedule = (Schedule)validationContext.ObjectInstance;

            
                //TimeSpan startTime, endTime;

                //startTime = (TimeSpan)schedule.StartTime;
                //endTime = (TimeSpan)schedule.EndTime;

                if (schedule.StartTime < schedule.EndTime)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("StartTime must be less");
                }
            
        }
    }
}