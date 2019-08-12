using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Vidly_Web_Application.Models;

namespace Vidly_Web_Application.Controllers.Api
{
    public class SemestersController : ApiController
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: api/Semesters
        public IQueryable<Semester> GetSemesters()
        {
            return db.Semesters;
        }

        // GET: api/Semesters/5
        [ResponseType(typeof(Semester))]
        public IHttpActionResult GetSemester(string id)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return NotFound();
            }

            return Ok(semester);
        }

        // PUT: api/Semesters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSemester(string id, Semester semester)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != semester.SemesterCode)
            {
                return BadRequest();
            }

            db.Entry(semester).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SemesterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Semesters
        [ResponseType(typeof(Semester))]
        public IHttpActionResult PostSemester(Semester semester)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Semesters.Add(semester);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SemesterExists(semester.SemesterCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = semester.SemesterCode }, semester);
        }

        // DELETE: api/Semesters/5
        [ResponseType(typeof(Semester))]
        public IHttpActionResult DeleteSemester(string id)
        {
            Semester semester = db.Semesters.Find(id);
            if (semester == null)
            {
                return NotFound();
            }

            db.Semesters.Remove(semester);
            db.SaveChanges();

            return Ok(semester);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SemesterExists(string id)
        {
            return db.Semesters.Count(e => e.SemesterCode == id) > 0;
        }
    }
}