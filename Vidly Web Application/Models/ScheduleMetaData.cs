using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(ScheduleMetaData))]
    public partial class Schedule
    {
    }

    public class ScheduleMetaData
    {
        [Required]
        public string SubjectCode { get; set; }

        [Required]
        public string SemesterCode { get; set; }

        [Required]
        public string DepartmentCode { get; set; }

        [Required]
        public string ClassDay { get; set; }

        
        [TimeValidator]
        [Required]
        public Nullable<System.TimeSpan> StartTime { get; set; }

        [Required]
        public Nullable<System.TimeSpan> EndTime { get; set; }

        [Required]
        public string Venue { get; set; }
        public bool ClassFlag { get; set; }

        [Required]
        [scheduleValidator]
        public string StaffID { get; set; }
        public int id { get; set; }
    }
}