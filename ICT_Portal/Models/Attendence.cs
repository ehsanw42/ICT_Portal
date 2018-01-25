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
    
    public partial class Attendence
    {
        public int ID { get; set; }
        public Nullable<int> EnrollmentID { get; set; }
        public Nullable<int> uID { get; set; }
        public string ClassRoom { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> FromTime { get; set; }
        public Nullable<System.DateTime> ToTime { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string TopicsCovered { get; set; }
    
        public virtual Enrollment Enrollment { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual User User { get; set; }
    }
}
