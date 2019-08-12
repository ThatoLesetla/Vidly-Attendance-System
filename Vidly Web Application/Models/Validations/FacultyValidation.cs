using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models.Validations
{
    public class FacultyValidation : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var faculty = (Faculty)validationContext.ObjectInstance;

            var fac = db.Faculties.Find(faculty.FacultyCode);

            if (fac == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Faculty Code already exits");
            }
        }
    }
}