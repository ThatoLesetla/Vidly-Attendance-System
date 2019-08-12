using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department
    { 
    }

    public class DepartmentMetaData
    {
        [Required]
        [StringLength(6)]
        [recordValidator]
        public string DepartmentCode { get; set; }

        [StringLength(50)]
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only can letters")]
        public string DepartmentName { get; set; }

        [StringLength(5)]
        [Required]
        public string FacultyCode { get; set; }
    }
}