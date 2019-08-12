﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Vidly_Web_Application.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class VidlyDbContext : DbContext
    {
        public VidlyDbContext()
            : base("name=VidlyDbContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VidlyMessage> VidlyMessages { get; set; }
    
        public virtual ObjectResult<spLogin_Result> spLogin(string email, string pass)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passParameter = pass != null ?
                new ObjectParameter("pass", pass) :
                new ObjectParameter("pass", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spLogin_Result>("spLogin", emailParameter, passParameter);
        }
    
        public virtual ObjectResult<spLogin_Result> SP_Staff_Insert(string staffID, string staffName, string staffSurname, string staffEmail, string staffPhone, string staffStreet, string staffCity, string staffPostalCode, Nullable<System.DateTime> staffHireDate, string semesterCode, string staffOffice, string staffDepartmentCode, string staffDesignation)
        {
            var staffIDParameter = staffID != null ?
                new ObjectParameter("StaffID", staffID) :
                new ObjectParameter("StaffID", typeof(string));
    
            var staffNameParameter = staffName != null ?
                new ObjectParameter("StaffName", staffName) :
                new ObjectParameter("StaffName", typeof(string));
    
            var staffSurnameParameter = staffSurname != null ?
                new ObjectParameter("StaffSurname", staffSurname) :
                new ObjectParameter("StaffSurname", typeof(string));
    
            var staffEmailParameter = staffEmail != null ?
                new ObjectParameter("StaffEmail", staffEmail) :
                new ObjectParameter("StaffEmail", typeof(string));
    
            var staffPhoneParameter = staffPhone != null ?
                new ObjectParameter("StaffPhone", staffPhone) :
                new ObjectParameter("StaffPhone", typeof(string));
    
            var staffStreetParameter = staffStreet != null ?
                new ObjectParameter("StaffStreet", staffStreet) :
                new ObjectParameter("StaffStreet", typeof(string));
    
            var staffCityParameter = staffCity != null ?
                new ObjectParameter("StaffCity", staffCity) :
                new ObjectParameter("StaffCity", typeof(string));
    
            var staffPostalCodeParameter = staffPostalCode != null ?
                new ObjectParameter("StaffPostalCode", staffPostalCode) :
                new ObjectParameter("StaffPostalCode", typeof(string));
    
            var staffHireDateParameter = staffHireDate.HasValue ?
                new ObjectParameter("StaffHireDate", staffHireDate) :
                new ObjectParameter("StaffHireDate", typeof(System.DateTime));
    
            var semesterCodeParameter = semesterCode != null ?
                new ObjectParameter("SemesterCode", semesterCode) :
                new ObjectParameter("SemesterCode", typeof(string));
    
            var staffOfficeParameter = staffOffice != null ?
                new ObjectParameter("StaffOffice", staffOffice) :
                new ObjectParameter("StaffOffice", typeof(string));
    
            var staffDepartmentCodeParameter = staffDepartmentCode != null ?
                new ObjectParameter("StaffDepartmentCode", staffDepartmentCode) :
                new ObjectParameter("StaffDepartmentCode", typeof(string));
    
            var staffDesignationParameter = staffDesignation != null ?
                new ObjectParameter("StaffDesignation", staffDesignation) :
                new ObjectParameter("StaffDesignation", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spLogin_Result>("SP_Staff_Insert", staffIDParameter, staffNameParameter, staffSurnameParameter, staffEmailParameter, staffPhoneParameter, staffStreetParameter, staffCityParameter, staffPostalCodeParameter, staffHireDateParameter, semesterCodeParameter, staffOfficeParameter, staffDepartmentCodeParameter, staffDesignationParameter);
        }
    
        public virtual ObjectResult<spLogin_Result> SP_Subject_Insert(string subjectCode, string subjectName, string departmentCode, Nullable<double> credits)
        {
            var subjectCodeParameter = subjectCode != null ?
                new ObjectParameter("SubjectCode", subjectCode) :
                new ObjectParameter("SubjectCode", typeof(string));
    
            var subjectNameParameter = subjectName != null ?
                new ObjectParameter("SubjectName", subjectName) :
                new ObjectParameter("SubjectName", typeof(string));
    
            var departmentCodeParameter = departmentCode != null ?
                new ObjectParameter("DepartmentCode", departmentCode) :
                new ObjectParameter("DepartmentCode", typeof(string));
    
            var creditsParameter = credits.HasValue ?
                new ObjectParameter("Credits", credits) :
                new ObjectParameter("Credits", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spLogin_Result>("SP_Subject_Insert", subjectCodeParameter, subjectNameParameter, departmentCodeParameter, creditsParameter);
        }
    
        public virtual int emailOTP(string userEmail)
        {
            var userEmailParameter = userEmail != null ?
                new ObjectParameter("UserEmail", userEmail) :
                new ObjectParameter("UserEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("emailOTP", userEmailParameter);
        }
    }
}