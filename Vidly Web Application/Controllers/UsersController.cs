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
using System.Web.Security;

namespace Vidly_Web_Application.Controllers
{
    public class UsersController : Controller
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(u => u.Staff);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.Username = new SelectList(db.Staffs, "StaffID", "StaffID");
            ViewBag.UserEmail = new SelectList(db.Staffs, "StaffEmail", "StaffEmail");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserEmail,Username,UserPassword,UserStatus,UserRole,UserIP,SecurityQuestion,SecurityAnswer")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "SHA1");
                    user.UserStatus = false;
                    user.UserIP = "127.0.0.1";

                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }

            ViewBag.Username = new SelectList(db.Staffs, "StaffID", "StaffID", user.Username);
            ViewBag.UserEmail = new SelectList(db.Staffs, "StaffEmail", "StaffEmail", user.UserEmail);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Username = new SelectList(db.Staffs, "StaffID", "StaffName", user.Username);
                return View(user);
            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserEmail,Username,UserPassword,UserStatus,UserRole,UserIP,SecurityQuestion,SecurityAnswer")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.UserPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UserPassword, "SHA1");
                    db.Entry(user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.Username = new SelectList(db.Staffs, "StaffID", "StaffName", user.Username);
                return View(user);
            } catch (Exception Ex)
            {
                ViewBag.Error = Ex.Message;
                return View("Error");
            }
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            try
            {

                User user = await db.Users.FindAsync(id);
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            } catch (Exception Ex)
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
