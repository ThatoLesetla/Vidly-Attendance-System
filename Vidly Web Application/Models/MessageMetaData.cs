using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly_Web_Application.Models
{
    [MetadataType(typeof(MessageMetaData))]
    public partial class VidlyMessage
    {
    }
    public class MessageMetaData
    {
        public string creatorID { get; set; }
        [Required]
        public string msgSubject { get; set; }
        [Required]
        public string msgBody { get; set; }
        public Nullable<System.DateTime> msgDate { get; set; }
        public Nullable<int> parentMsgID { get; set; }
    }
}