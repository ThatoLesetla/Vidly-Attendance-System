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
    
    public class StaffMembersController : Controller
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: StaffMembers
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Department).Include(s => s.Semester);
            return View(staffs.ToList());
        }

        // GET: StaffMembers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: StaffMembers/Create
        public ActionResult Create()
        {
            ViewBag.StaffDepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName");
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc");
            ViewBag.OfficeCode = new SelectList(db.Locations, "RoomNo", "RoomNo");

            return View();
        }

        // POST: StaffMembers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,StaffName,StaffSurname,StaffEmail,StaffPhone,StaffStreet,StaffCity,StaffPostalCode,StaffHireDate,SemesterCode,StaffOffice,StaffDepartmentCode,StaffDesignation")] Staff staff)
        {
            //int length;
            var obj = db.Staffs.Find(staff.StaffID);
            //length = staff.StaffID.Length;

            try
            {

                if (ModelState.IsValid)
                {

                    if (obj == null)
                    {

                        // db.Staffs.Add(staff);
                        staff.StaffHireDate = DateTime.Now;
                        var staffOffice = db.Staffs.ToList();
                        var emptyOffice = db.Locations.Where(a => a.RoomType.Equals("Office"));

                        foreach (var item in staffOffice)
                        {
                            foreach (var item2 in emptyOffice)
                            {
                                if (item.StaffOffice != item2.RoomNo)
                                {
                                    staff.StaffOffice = item2.RoomNo;
                                    break;
                                }

                            }
                        }

                        if (staff.StaffOffice == null)
                        {
                            ModelState.AddModelError("", "The are no available offices for lecturer");
                        }

                        db.SP_Staff_Insert(staff.StaffID, staff.StaffName, staff.StaffSurname, staff.StaffEmail, staff.StaffPhone, staff.StaffStreet, staff.StaffCity, staff.StaffPostalCode, staff.StaffHireDate, staff.SemesterCode, staff.StaffOffice, staff.StaffDepartmentCode, staff.StaffDesignation);
                        db.SaveChanges();
                        return RedirectToAction("Create", "Users");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Staff ID already exits");
                    }
                }

            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }
            ViewBag.StaffDepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", staff.StaffDepartmentCode);
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc", staff.SemesterCode);
            ViewBag.OfficeCode = new SelectList(db.Locations, "RoomNo", "RoomNo", staff.StaffOffice);

            return View(staff);
        }

        // GET: StaffMembers/Edit pass id to function
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffDepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", staff.StaffDepartmentCode);
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc", staff.SemesterCode);
            return View(staff);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,StaffName,StaffSurname,StaffEmail,StaffPhone,StaffStreet,StaffCity,StaffPostalCode,StaffHireDate,SemesterCode,StaffOffice,StaffDepartmentCode,StaffDesignation")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffDepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", staff.StaffDepartmentCode);
            ViewBag.SemesterCode = new SelectList(db.Semesters, "SemesterCode", "SemesterDesc", staff.SemesterCode);
            return View(staff);
        }

        // GET: StaffMembers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: StaffMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Staff staff = db.Staffs.Find(id);

            User user = db.Users.Find(staff.StaffEmail);

            try
            {

                if (user != null)
                {
                    db.Users.Remove(user);
                }
                else
                {
                    user = null;
                }

                db.Staffs.Remove(staff);
                db.SaveChanges();

            } catch(Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }

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
