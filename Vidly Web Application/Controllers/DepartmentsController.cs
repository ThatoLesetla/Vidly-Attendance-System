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

namespace Vidly_Web_Application.Controllers
{
    public class DepartmentsController : Controller
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: Departments
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.Faculty);
            
            return View(await departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.FacultyCode = new SelectList(db.Faculties, "FacultyCode", "Name");
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentCode,DepartmentName,FacultyCode")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FacultyCode = new SelectList(db.Faculties, "FacultyCode", "Name", department.FacultyCode);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacultyCode = new SelectList(db.Faculties, "FacultyCode", "Name", department.FacultyCode);
            return View(department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepartmentCode,DepartmentName,FacultyCode")] Department department)
        {
           try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(department).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.FacultyCode = new SelectList(db.Faculties, "FacultyCode", "Name", department.FacultyCode);
                return View(department);
            } catch (Exception Ex)
            {
                return View("Error");
            }
        }

        // GET: Departments/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {

                Department department = await db.Departments.FindAsync(id);
                db.Departments.Remove(department);
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message.ToString();
                return View("Error");
            }
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
