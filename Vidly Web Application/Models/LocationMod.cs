using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models.Validations;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(LocationMetaData))]
    public partial class Location
    {
    }


    public class LocationMetaData
    {
        [StringLength(8)]
        [Required]
        [LocationValidator]
        public string RoomNo { get; set; }

        [Required]
        [Range(1, 50)]
        public string Building { get; set; }

        [Required]
        [Range(1, 200)]
        public Nullable<int> Capacity { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "This entry can only contain letters")]
        public string RoomType { get; set; }
    }

}