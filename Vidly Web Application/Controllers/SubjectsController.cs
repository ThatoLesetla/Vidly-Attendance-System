using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vidly_Web_Application.Models;
using System.Data.SqlClient;

namespace Vidly_Web_Application.Controllers
{
    public class SubjectsController : Controller
    {
        private VidlyDbContext db = new VidlyDbContext();
        
        // GET: Subjects
        public async Task<ActionResult> Index()
        {
                 var subjects = db.Subjects.Include(s => s.Department);
                 return View(await subjects.ToListAsync());
          
        }

        // GET: Subjects/Details/pass parameter
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName");
            return View();
        }

        // POST: Subjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SubjectCode,SubjectName,DepartmentCode,Credits")] Subject subject)
        {
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", subject.DepartmentCode);

            try
            {

                if (ModelState.IsValid)
                {

                    db.Subjects.Add(subject);
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }

            } catch(Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }
            
            return View(subject);
        }

        // GET: Subjects/Edit/pass parameter
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", subject.DepartmentCode);
            return View(subject);
        }

        // POST: Subjects/Edit/pass parameter
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "SubjectCode,SubjectName,DepartmentCode,Credits")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentCode = new SelectList(db.Departments, "DepartmentCode", "DepartmentName", subject.DepartmentCode);
            return View(subject);
        }

        // GET: Subjects/Delete/pass parameter
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = await db.Subjects.FindAsync(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/pass parameter
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Subject subject = await db.Subjects.FindAsync(id);

            try
            {

                db.Subjects.Remove(subject);
                await db.SaveChangesAsync();

            } catch(Exception Ex)
            {
                ViewBag.Error = Ex.Message.ToString() ;
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
