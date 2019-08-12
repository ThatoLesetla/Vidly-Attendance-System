using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models
{
    public class recordValidator : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var department = (Department)validationContext.ObjectInstance;
            
            var deptRecord = db.Departments.Find(department.DepartmentCode);
            
            
            if(deptRecord == null)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("Record already exits");
            }

            
        }
    }
}