using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(SemesterMetaData))]
    public partial class Semester
    {
    }

    public class SemesterMetaData
    {
        [Required]
        [StringLength(4)]
        public string SemesterCode { get; set; }

        [Required]
        [StringLength(30)]
        public string SemesterDesc { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime EndDate { get; set; }
    }
}