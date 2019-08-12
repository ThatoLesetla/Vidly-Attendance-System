using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Vidly_Web_Application.Models;

namespace Vidly_Web_Application.Controllers
{
    public class SchedulesController : Controller
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: Schedules
        public ActionResult Index(string id)
        {
            String staffID = Session["Username"].ToString();
            String role = Session["UserRole"].ToString();

            if (role == "admin")
            {
                var schedules = db.Schedules.Include(s => s.Location).Include(s => s.Semester).Include(s => s.Subject);
                schedules.ToList();

                return View(schedules);
            } else
            {
                var schedules = db.Schedules.Where(a => a.StaffID.Equals(staffID));
                schedules.ToList();

                return View(schedules);
            }
      
        }

        public ActionResult MyTable()
        {
           
            string id = Session["username"].ToString();

            // List<SelectList> subjects = new SelectList(db.Schedules.Where(a => a.StaffID.Equals(id), );
    
            var mySchedule = db.Schedules.Where(a => a.StaffID.Equals(id));

            return View(mySchedule.ToList());
        }

        // GET: Schedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: Schedules/Create
        public ActionResult Create()
        {
            ViewBag.Venue = new SelectList(db.Locations, "RoomNo", "RoomNo");
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc");
            ViewBag.SubjectCode = new SelectList(db.Subjects, "SubjectCode", "SubjectCode");
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentCode");
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "StaffID");
            
            return View();
        }

        // POST: Schedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectCode,SemesterCode,DepartmentCode,ClassDay,StartTime,EndTime,Venue,ClassFlag,StaffID,id")] Schedule schedule)
        {
            //TimeSpan startTime, endTime;

            //startTime = (TimeSpan)schedule.StartTime;
            //endTime = (TimeSpan)schedule.EndTime;

            //if (startTime > endTime)
            //{
            //    return Content("The validation is working");
            //}

            try
            {

                if (ModelState.IsValid)
                {
                    //Subject subject = db.Subjects.Find(schedule.SubjectCode);

                    //schedule.DepartmentCode = subject.DepartmentCode;
                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Venue = new SelectList(db.Locations, "RoomNo", "Building", schedule.Venue);
                ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc", schedule.SemesterCode);
                ViewBag.SubjectCode = new SelectList(db.Subjects, "SubjectCode", "SubjectName", schedule.SubjectCode);
                ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentCode");
                ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "StaffID");

                return View(schedule);
            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }
        }

        // GET: Schedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
         
            ViewBag.Venue = new SelectList(db.Locations, "RoomNo", "RoomNo", schedule.Venue);
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc", schedule.SemesterCode);
            ViewBag.SubjectCode = new SelectList(db.Subjects, "SubjectCode", "SubjectName", schedule.SubjectCode);
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", schedule.DepartmentCode);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "StaffID", schedule.StaffID);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectCode,SemesterCode,DepartmentCode,ClassDay,StartTime,EndTime,Venue,ClassFlag,StaffID,id")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Venue = new SelectList(db.Locations, "RoomNo", "Building", schedule.Venue);
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc", schedule.SemesterCode);
            ViewBag.SubjectCode = new SelectList(db.Subjects, "SubjectCode", "SubjectName", schedule.SubjectCode);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public ActionResult Stafftable()
        //{
        //    ViewBag.Lectures = new SelectList(db.Staffs, "StaffID", "StaffID");

        //    var table = db.Schedules.Where(a => a.StaffID.Equals())

        //    var schedules = db.Schedules.Where(a => a.StaffID.Equals(id)).Include(s => s.Location).Include(s => s.Semester).Include(s => s.Subject);
        //    return View();
        //}

        public ActionResult upcomingClass()
        {
            string startTime;

            startTime = DateTime.Now.ToString("T");

            return Content(startTime);
        }
    }
}
