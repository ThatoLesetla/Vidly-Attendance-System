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
    public class FacultiesController : ApiController
    {
        private VidlyDbContext db = new VidlyDbContext();

        // GET: api/Faculties
        public IQueryable<Faculty> GetFaculties()
        {
            return db.Faculties;
        }

        // GET: api/Faculties/5
        [ResponseType(typeof(Faculty))]
        public IHttpActionResult GetFaculty(string id)
        {
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return NotFound();
            }

            return Ok(faculty);
        }

        // PUT: api/Faculties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFaculty(string id, Faculty faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faculty.FacultyCode)
            {
                return BadRequest();
            }

            db.Entry(faculty).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultyExists(id))
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

        // POST: api/Faculties
        [ResponseType(typeof(Faculty))]
        public IHttpActionResult PostFaculty(Faculty faculty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Faculties.Add(faculty);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (FacultyExists(faculty.FacultyCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = faculty.FacultyCode }, faculty);
        }

        // DELETE: api/Faculties/5
        [ResponseType(typeof(Faculty))]
        public IHttpActionResult DeleteFaculty(string id)
        {
            Faculty faculty = db.Faculties.Find(id);
            if (faculty == null)
            {
                return NotFound();
            }

            db.Faculties.Remove(faculty);
            db.SaveChanges();

            return Ok(faculty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacultyExists(string id)
        {
            return db.Faculties.Count(e => e.FacultyCode == id) > 0;
        }
    }
}