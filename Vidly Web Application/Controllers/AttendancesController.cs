using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vidly_Web_Application.Models;

namespace Vidly_Web_Application.Controllers
{
    public class AttendancesController : Controller
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: Attendances
        public ActionResult Index()
        {
            var attendances = db.Attendances.Include(a => a.User);
            return View(attendances.ToList());
        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // GET: Attendances/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Users, "UserEmail", "Username");
            return View();
        }

        // POST: Attendance/AddRecord
        public ActionResult AddRecord(Attendance attendance)
        {
            db.Attendances.Add(attendance);
            db.SaveChanges();

            return View();
        }

        // POST: Attendances/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userId,attendanceHours,attendanceDate")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Users, "UserEmail", "Username", attendance.userId);
            return View(attendance);
        }

        // GET: Attendances/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Users, "UserEmail", "Username", attendance.userId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userId,attendanceHours,attendanceDate")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Users, "UserEmail", "Username", attendance.userId);
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance attendance = db.Attendances.Find(id);
            if (attendance == null)
            {
                return HttpNotFound();
            }
            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance attendance = db.Attendances.Find(id);
            db.Attendances.Remove(attendance);
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
    }
}
