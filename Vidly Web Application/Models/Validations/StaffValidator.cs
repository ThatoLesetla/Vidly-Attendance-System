using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models.Validations
{
    public class StaffValidator : ValidationAttribute
    {
        VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var staff = (Staff)validationContext.ObjectInstance;

            var staffInfo = db.Staffs.Find(staff.StaffID);

            if(staffInfo == null)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("Staff ID already exits");
            }
        }
    }
}