using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.DTOs
{
    public class StaffDTO
    {
        [Required]
        [StringLength(9)]
        [RegularExpression(@"^[0-9''-'\s]{1,40}$", ErrorMessage = "Enter valid staff number")]
        public string StaffID { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffSurname { get; set; }

        [Required]
        [StringLength(40)]
        [EmailAddress]
        public string StaffEmail { get; set; }

        [Required]
        [Phone]
        [StringLength(10)]
        [DataType(DataType.PhoneNumber)]
        public string StaffPhone { get; set; }

        [Required]
        [StringLength(20)]
        public string StaffStreet { get; set; }

        [Required]
        [StringLength(40)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffCity { get; set; }

        [Required]
        [StringLength(4)]
        [RegularExpression(@"^[0-9''-'\s]{1,40}$", ErrorMessage = "This entry can only contain numbers")]
        public string StaffPostalCode { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime StaffHireDate { get; set; }

        [Required]
        public string SemesterCode { get; set; }

        [StringLength(7)]
        public string StaffOffice { get; set; }

        [Required]
        public string StaffDepartmentCode { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string StaffDesignation { get; set; }
    }
}