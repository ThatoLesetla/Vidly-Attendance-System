using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models.Validations;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(SubjectMetaData))]
    public partial class Subject
    {
    }

    public class SubjectMetaData
    {
        [Required]
        [StringLength(8)]
        [DataType(DataType.Text)]
        [SubjectValidator]
        public string SubjectCode { get; set; }

        [Required]
        [StringLength(45)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string SubjectName { get; set; }

        [Required]
        [StringLength(6)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string DepartmentCode { get; set; }

        [Required]
        [Range(1, 450)]
        public double Credits { get; set; }
    }
}