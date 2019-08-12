using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly_Web_Application.Models;

namespace Vidly_Web_Application.ViewModels.Home
{
    public class DashboardViewModel
    {
        public List<Staff> staffs { get; set; }
        public List<User> users { get; set; }
        public int numStaff { get; set; }
        public double cos { get; set; }
        public double ai { get; set; }
        public double ictfu { get; set; }
    }
}