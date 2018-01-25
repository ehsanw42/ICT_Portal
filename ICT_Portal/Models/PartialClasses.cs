using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICT_Portal.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        [Display(Name = "New Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("RePassword2")]
        public string RePassword { get; set; }

        [Display(Name = "Re-Enter New Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("RePassword")]
        public string RePassword2 { get; set; }
    }

         [MetadataType(typeof(AttendenceMetadata))]
        public partial class Attendence
        {
        }

        [MetadataType(typeof(BatchMetadata))]
        public partial class Batch
        {
        }

        [MetadataType(typeof(CourseMetadata))]
        public partial class Course
        { 
        }

        [MetadataType(typeof(DepartmentMetadata))]
        public partial class Department
        {
        }

        [MetadataType(typeof(EnrollmentMetadata))]
        public partial class Enrollment
        {
        }

        [MetadataType(typeof(InstructorMetadate))]
        public partial class Instructor
        {
            [NotMapped]
            [ScaffoldColumn(false)]
            [Required(ErrorMessage = "Please Specify Username")]
            [Display(Name = "User Name")]
            public string Username { get; set; }
            [NotMapped]
            [ScaffoldColumn(false)]
            [Required(ErrorMessage = "Please Specify Password")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        [MetadataType(typeof(InstructorCoursMetadata))]
        public partial class InstructorCours
        {
        }

        [MetadataType(typeof(SectionMetadata))]
        public partial class Section
        {
        }

        [MetadataType(typeof(StudentMetadata))]
        public partial class Student
        {
        }
        [MetadataType(typeof(AssessmentMetadata))]
        public partial class Assessment
        {
        }

    }
