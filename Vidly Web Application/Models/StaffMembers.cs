using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models.Validations;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(StaffMetaData))]
    public partial class Staff
    {
    }

    public class StaffMetaData
    {
        [Required]
        [StringLength(9)]
        [Display(Name = "ID")]
        [RegularExpression(@"^[0-9''-'\s]{1,40}$", ErrorMessage = "Enter valid staff number")]
        [StaffValidator]
        public string StaffID { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Surname")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffSurname { get; set; }

        [Required]
        [StringLength(40)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string StaffEmail { get; set; }

        [Required]
        [Phone]
        [StringLength(10)]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string StaffPhone { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Street")]
        public string StaffStreet { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "City")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffCity { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name = "PostalCode")]
        [RegularExpression(@"^[0-9''-'\s]{1,40}$", ErrorMessage = "This entry can only contain numbers")]
        public string StaffPostalCode { get; set; }

        [Required]
        [Display(Name = "Hiredate")]
        [DataType(DataType.Date)]
        public System.DateTime StaffHireDate { get; set; }

        [Required]
        [Display(Name = "SemesterCode")]
        public string SemesterCode { get; set; }

        [Display(Name = "Office")]
        [StringLength(7)]
        public string StaffOffice { get; set; }

        [Required]
        [Display(Name = "DepartmentCode")]
        public string StaffDepartmentCode { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Designation")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffDesignation { get; set; }

    }
}