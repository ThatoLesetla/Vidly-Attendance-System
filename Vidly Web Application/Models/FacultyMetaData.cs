using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models.Validations;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(FacultyMetaData))]
    public partial class Faculty
    {
    }

    public class FacultyMetaData
    {
        [Required]
        [StringLength(5)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This field reguires letters only")]
        [FacultyValidation]
        public string FacultyCode { get; set; }

        [Required]
        [StringLength(45)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This field reguires letters only")]
        public string Name { get; set; }

        [Required]
        [StringLength(6)]
        public string Building { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This field reguires letters only")]
        public string Campus { get; set; }

        [Required]
        [StringLength(10)]
        [Phone]
        public string Phone { get; set; }
    }
}