using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models;

namespace Vidly_Web_Application.ViewModels.Home
{
    public class staffReportView
    {
        public Staff staff { get; set; }
        public List<Staff> staffs { get; set; }
    }
}