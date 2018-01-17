//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ICT_Portal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Instructor
    {
        public Instructor()
        {
            this.Assessments = new HashSet<Assessment>();
            this.Attendences = new HashSet<Attendence>();
            this.InstructorCourses = new HashSet<InstructorCours>();
        }
    
        public int ID { get; set; }
        public Nullable<int> uID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string Gender { get; set; }
        public string Designation { get; set; }
        public Nullable<int> DeptID { get; set; }
        public string Email { get; set; }
        public string DeptPosition { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentCity { get; set; }
        public string PermanentCity { get; set; }
        public Nullable<int> ExperienceYear { get; set; }
        public Nullable<int> ExperienceMonth { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public Nullable<System.DateTime> ResignationDate { get; set; }
        public byte[] Photo { get; set; }
        [ScaffoldColumn(false)]
        public string Status { get; set; }
        public string  Username { get; set; }
        public string Password { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual ICollection<Assessment> Assessments { get; set; }
        public virtual ICollection<Attendence> Attendences { get; set; }
        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<InstructorCours> InstructorCourses { get; set; }
    }
}
