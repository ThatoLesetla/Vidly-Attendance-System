using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using Vidly_Web_Application.Models;
using System.Data.Entity;
using System.Net.Sockets;
using System.Net.Mail;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Data;
using Vidly_Web_Application.ViewModels.Home;
using System.Web.UI.WebControls;

namespace Vidly_Web_Application.Controllers
{
    public class HomeController : Controller
     {
        VidlyDbContext db = new VidlyDbContext();

        
        public ActionResult Login()
        { 
           
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Obsolete]
        // This method receives the login details which are entered by the user
        public ActionResult Login(User userObj)
        {
            string address, hostname;  
            string password = userObj.UserPassword;
            
            Attendance attendance = new Attendance(); // When a user logs into the system his/her attendance is immediately recorded
            Random rand = new Random(); // Random number must be generated for the user to verify his One Time Pin as a way of Two Factor authentication

            int otp;        // One Time Pin
           

            if (ModelState.IsValid)
            {
                // All user passwords are hashed:  "HashPasswordForStoringInConfigFile" hashes the password and finds a match in the database
                password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                userObj.UserPassword = password;
                
                // A match of the entered details must be searched on the database
                var obj = db.Users.Where(a => a.UserEmail.Equals(userObj.UserEmail) && a.UserPassword.Equals(userObj.UserPassword)).FirstOrDefault();

                // Login receives userObject whereas user object requires fields to be instantiated. Therefor assign obj to userobj
                userObj = obj;

                if (obj != null)
                {
                    Session["UserEmail"] = obj.UserEmail;
                    Session["Username"] = obj.Username;
                    Session["UserRole"] = obj.UserRole;
                    Session["Name"] = obj.Staff.StaffName;
                    Session["LoginTime"] = double.Parse(DateTime.Now.TimeOfDay.TotalMinutes.ToString());
                    Session.Timeout = 10800;
                    otp = rand.Next(1000, 9999);
                    obj.OTP = otp.ToString();
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();

                    attendance.userId = obj.UserEmail;

                    hostname = Dns.GetHostName();
                    address = Dns.GetHostByName(hostname).AddressList[0].ToString();
                    obj.UserIP = address;

                    // Flag the user as online
                    obj.UserStatus = true;

                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("SP_emailOTP @UserEmail", new SqlParameter("@UserEmail", obj.UserEmail));

                    return RedirectToAction("OneTimePin");

                } else {
                    ModelState.AddModelError("LoginError", "User does not exist on our records");
                    return View();
                }
                      
            }

            return View();

        }

        [HttpGet]
        public ActionResult OneTimePin()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult OneTimePin(FormCollection form)
        {
            string otp;
            User user;

            otp = form["otp"];
            user = db.Users.Find(Session["UserEmail"].ToString());

            try
            {

                if (user.OTP != otp)
                {
                    return View();
                }
            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }

        return RedirectToAction("UserDashboard");

           
        }

      [HttpGet]
      public ActionResult SignUp()
        {

            return View();
        }

        [HttpPost]
        public ActionResult SignUp(User user)
        {

            var obj = db.Staffs.Find(user.Username);
            try
            {

                if (ModelState.IsValid)
                {
                    if(obj != null)
                    {
                        db.Users.Add(user);
                        db.SaveChanges();

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User doesn't exist on our database");
                    }
                }

            } catch(Exception Ex)
            {
                return View("Error" + Ex.HelpLink);
            }

            return RedirectToAction("SignUp");
        }

        public ActionResult Reports(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/VidlyReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "LecturerDataSet";
            reportDataSource.Value = db.Staffs.ToList();
            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            if(reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderByte;

            renderByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("content-disposition", "attachment;filename=lecture_report." + fileNameExtension);

            return File(renderByte, fileNameExtension);

        }

        public ActionResult SummaryReports(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/SummaryReport.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "LecturerAttendanceDataSet";
            reportDataSource.Value = db.Staffs.ToList();
            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension;

            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }
            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }

            string[] streams;
            Warning[] warnings;
            byte[] renderByte;

            renderByte = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            Response.AddHeader("content-disposition", "attachment;filename=lecture_report." + fileNameExtension);

            return File(renderByte, fileNameExtension);

        }

        public ActionResult Logout(string id)
        {
            User loggedin;
            Attendance attendance = new Attendance();
            double hours, loginTime, logoutTime;

            loggedin = db.Users.Find(id);

            loggedin.UserStatus = false;

            logoutTime = double.Parse(DateTime.Now.TimeOfDay.TotalMinutes.ToString());
            loginTime = double.Parse(Session["LoginTime"].ToString());

            hours = (logoutTime - loginTime) / 60;

            
 
            attendance.userId = Session["UserEmail"].ToString();
            attendance.attendanceHours = hours;
            attendance.attendanceDate = DateTime.Now;

            // attendance.attendanceHours = hours;
          
            db.Attendances.Add(attendance);
            db.Entry(loggedin).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Login");
            //return Content(hours.ToString());
        }
    
        public ActionResult ForgotPassword()
        {

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ForgotPassword(User user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("vidlymanagement@gmail.com");
                    mail.To.Add("tlesetla6198@gmail.com");
                    mail.Subject = "Forgot Password";
                    var objUser = db.Users.Where(a => a.Username.Equals(user.Username)).FirstOrDefault();
                    mail.Body = "Your password is: " + objUser.UserPassword;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("vidlymanagement@gmail.com", "34Tg02Ymsr$");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                } catch(Exception ex)
                {
                    return Content(ex.ToString());
                }

            }
            return RedirectToAction("Login");
        }
        public ActionResult UserDashboard(FormCollection collection)
        {
            List<int> numStaff = new List<int>();

            if (Session["UserRole"] != null)
            {
                List<User> users = db.Users.Where(a => a.UserStatus == true).ToList();
                 
                //for(int i = 0; i < departments.Count; i++)
                //{
                //    department = departments[i].DepartmentCode;

                //    for(int m = 0; m < staffs.Count; m++)
                //    {
                //        if(staffs[m].StaffDepartmentCode == department)
                //        {
                //            total = total + 1;
                //        }
                //    }

                //    numStaff[i] = total;
                //}
                
                ViewBag.staff = numStaff;
                return View(users);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        public ActionResult SummaryReport()
        {
            try {
                string username;
                username = Session["Username"].ToString();
                var newUser = db.Staffs.Find(username);
                double hours = 0;
                TimeSpan numDays;

                ViewBag.StaffAttendance = new SelectList(db.Attendances);
                ViewBag.StaffAttendance = new SelectList(db.Schedules);
                List<Attendance> attendance = db.Attendances.ToList();

                if (newUser != null)
                {
                    numDays = DateTime.Now.Subtract(newUser.StaffHireDate);
                    foreach (var item in attendance)
                    {
                        if (newUser.StaffEmail == item.userId)
                        {
                            hours = hours + (double)item.attendanceHours;
                        }
                    }

                    ViewBag.hoursWorked = hours;
                    ViewBag.daysWorked = numDays;
                }

                return View(newUser);

            } catch(Exception Ex)
            {
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult SummaryReport(Staff staff)
        {
            Staff newUser;
            double hours = 0;
            TimeSpan numDays;
            double classes;

           // ViewBag.StaffMembers = new SelectList(db.Staffs, "StaffID")
            newUser = db.Staffs.Find(staff.StaffID);

            List<Attendance> attendance = db.Attendances.ToList();

            if(newUser != null)
            {
                numDays = DateTime.Now.Subtract(newUser.StaffHireDate);

                foreach (var item in attendance)
                {
                    if (newUser.StaffEmail == item.userId)
                    {
                        hours = hours + (double)item.attendanceHours;
                    }
                }

                classes = (hours * 60) / 90;

                ViewBag.hoursWorked = hours;
                ViewBag.daysWorked = numDays;
                ViewBag.totClasses = classes;
                
            } else
            {
                ModelState.AddModelError("", "User does not exist");

                return View();
            }

            return View(newUser);
        }

       
        public ActionResult StaffDetailReport()
        {
            
            ViewBag.StaffDepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc");
            ViewBag.OfficeCode = new SelectList(db.Locations, "RoomNo", "RoomNo");

            var staffList = db.Staffs.Include(s => s.Department).Include(s => s.Semester);
            //ViewBag.Department = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", dept.DepartmentName);

            staffReportView reportView = new staffReportView
            {
                staff = new Staff(),
                staffs = staffList.ToList()
            };

            return View(reportView);
        }

        [HttpPost]
        public ActionResult StaffDetailReport(FormCollection formObject)
        {
            string department, designation;
            DateTime startDate, fromDate;
            ViewBag.StaffDepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc");
            ViewBag.OfficeCode = new SelectList(db.Locations, "RoomNo", "RoomNo");

            department = formObject["StaffDepartmentCode"];
            startDate = Convert.ToDateTime(formObject["StartDate"]);
            fromDate = Convert.ToDateTime(formObject["FromDate"]);
            designation = formObject["staff.StaffDesignation"];

            var staffList = db.Staffs.Where(a => a.StaffDepartmentCode.Equals(department) && a.StaffDesignation.Equals(designation));
            // staffList = staffList.Where(a => a.StaffHireDate >= startDate && a.StaffHireDate <= fromDate); a.
            
            

            staffReportView reportView = new staffReportView()
            {
                staff = new Staff(),
                staffs = staffList.ToList()
            };

            return View(reportView);
        }
        public ActionResult testReport()
        {
            return View();
        }
        public ActionResult AttendanceDetailReport()
        {
            var attendances = db.Attendances.Include(a => a.User);

            return View(attendances.ToList());
        }

        public ActionResult ExceptionReport()
        {
            return View();
        }


        public ActionResult ActiveProfiles()
        {
            List<User> users = db.Users.Where(a => a.UserStatus == true).ToList();

            return View(users);
        }

        public ActionResult InactiveProfiles()
        {
            return View();
        }

        public ActionResult Construction()
        {
            ViewBag.Message = "This page is under constructon.";

            return View();
        }


    }
}