using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models.Validations
{
    public class SubjectValidator : ValidationAttribute
    {
        private VidlyDbContext db = new VidlyDbContext();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var subjects = (Subject)validationContext.ObjectInstance;

            var subjectItem = db.Subjects.Find(subjects.SubjectCode);

            if(subjectItem == null)
            {
                return ValidationResult.Success;
            } else
            {
                return new ValidationResult("Subject already exits");
            }
        }
    }
}