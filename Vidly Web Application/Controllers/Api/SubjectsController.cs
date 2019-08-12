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
    public class SubjectsController : ApiController
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: api/Subjects
        public IQueryable<Subject> GetSubjects()
        {
            return db.Subjects;
        }

        // GET: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public IHttpActionResult GetSubject(string id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subject);
        }

        // PUT: api/Subjects/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSubject(string id, Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subject.SubjectCode)
            {
                return BadRequest();
            }

            db.Entry(subject).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [ResponseType(typeof(Subject))]
        public IHttpActionResult PostSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Subjects.Add(subject);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SubjectExists(subject.SubjectCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = subject.SubjectCode }, subject);
        }

        // DELETE: api/Subjects/5
        [ResponseType(typeof(Subject))]
        public IHttpActionResult DeleteSubject(string id)
        {
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }

            db.Subjects.Remove(subject);
            db.SaveChanges();

            return Ok(subject);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SubjectExists(string id)
        {
            return db.Subjects.Count(e => e.SubjectCode == id) > 0;
        }
    }
}