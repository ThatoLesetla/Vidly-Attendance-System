using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly_Web_Application.Models;
using Vidly_Web_Application.DTOs;
using AutoMapper;

namespace Vidly_Web_Application.Controllers.Api
{
    public class StaffsController : ApiController
    {
        private VidlyDbContext db;

        public StaffsController()
        {
            db = new VidlyDbContext();
        }

        // GET /api/staff
        public IEnumerable<StaffDTO> GetStaffs()
        {
            return db.Staffs.ToList().Select(Mapper.Map<Staff, StaffDTO>);
        }

        // GET /api/staffs/1
        public IHttpActionResult GetStaff(string id)
        {
            var staff = db.Staffs.SingleOrDefault(c => c.StaffID == id);

            if(staff == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Staff, StaffDTO>(staff));
        }

        // POST /api/staffs
        [HttpPost]
        public IHttpActionResult CreateStaff(StaffDTO staffDTO)
        {
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            // map the staff to the DTO
            var staff = Mapper.Map<StaffDTO, Staff>(staffDTO);

            // add it to the database
            db.Staffs.Add(staff);
            db.SaveChanges();

            staffDTO.StaffID = staff.StaffID;

            return Created(new Uri(Request.RequestUri + "/" + staff.StaffID), staffDTO);
        }

        // PUT /api/staff/1
        [HttpPut]
        public void UpdateStaff(string id, StaffDTO staffDTO)
        {
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var staffInDb = db.Staffs.SingleOrDefault(c => c.StaffID == id);

            if(staffInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(staffDTO, staffInDb);
           

            db.SaveChanges();

        }

        // DELETE /API/Staff/1

        public void DeleteStaff(string id)
        {
            var staffInDb = db.Staffs.SingleOrDefault(c => c.StaffID == id);
            var userInDb = db.Users.SingleOrDefault(c => c.Username == id);
            //var attenInDb = db.Attendances.SingleOrDefault(c => c.userId == id);
            if(staffInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);

            }

            //db.Attendances.Remove(attenInDb);
            db.Users.Remove(userInDb);
            db.Staffs.Remove(staffInDb);
            db.SaveChanges();
        }
    }
}
