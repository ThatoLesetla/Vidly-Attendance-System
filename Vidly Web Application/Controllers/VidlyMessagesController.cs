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
    public class VidlyMessagesController : Controller
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: VidlyMessages
        public ActionResult Index()
        {
            var vidlyMessages = db.VidlyMessages.Include(v => v.User);
            return View(vidlyMessages.ToList());
        }

        // GET: VidlyMessages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VidlyMessage vidlyMessage = db.VidlyMessages.Find(id);
            if (vidlyMessage == null)
            {
                return HttpNotFound();
            }
            return View(vidlyMessage);
        }

        // GET: VidlyMessages/Create
        public ActionResult Create()
        {
            ViewBag.creatorID = new SelectList(db.Users, "UserEmail", "Username");
            return View();
        }

        // POST: VidlyMessages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,creatorID,msgSubject,msgBody,msgDate,parentMsgID")] VidlyMessage vidlyMessage)
        {
            if (ModelState.IsValid)
            {
                vidlyMessage.creatorID = Session["UserEmail"].ToString();
                vidlyMessage.msgDate = DateTime.Now;
                db.VidlyMessages.Add(vidlyMessage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.creatorID = new SelectList(db.Users, "UserEmail", "Username", vidlyMessage.creatorID);
            return View(vidlyMessage);
        }

        // GET: VidlyMessages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VidlyMessage vidlyMessage = db.VidlyMessages.Find(id);
            if (vidlyMessage == null)
            {
                return HttpNotFound();
            }
            ViewBag.creatorID = new SelectList(db.Users, "UserEmail", "Username", vidlyMessage.creatorID);
            return View(vidlyMessage);
        }

        // POST: VidlyMessages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,creatorID,msgSubject,msgBody,msgDate,parentMsgID")] VidlyMessage vidlyMessage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vidlyMessage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.creatorID = new SelectList(db.Users, "UserEmail", "Username", vidlyMessage.creatorID);
            return View(vidlyMessage);
        }

        // GET: VidlyMessages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VidlyMessage vidlyMessage = db.VidlyMessages.Find(id);
            if (vidlyMessage == null)
            {
                return HttpNotFound();
            }
            return View(vidlyMessage);
        }

        // POST: VidlyMessages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VidlyMessage vidlyMessage = db.VidlyMessages.Find(id);
            db.VidlyMessages.Remove(vidlyMessage);
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
