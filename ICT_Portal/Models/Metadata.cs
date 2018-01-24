using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ICT_Portal.Models
{
    public class AttendenceMetadata
    {
        [Display(Name = "Class Room")]
        public string ClassRoom;
        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        public Nullable<System.DateTime> EntryDate;
        [DataType(DataType.Date)]
        [Display(Name = "Start Time")]
        public Nullable<System.DateTime> FromTime;
        [DataType(DataType.Date)]
        [Display(Name = "End Time")]
        public Nullable<System.DateTime> ToTime;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [Display(Name = "Topics Covered")]
        public string TopicsCovered;
    }

    public class AssessmentMetadata{
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
        [ScaffoldColumn(false)]
        public Nullable<int> enrollmentID;
        [Display(Name = "Assignment 1 Max Marks")]
        public Nullable<int> A1_Max;
        [Display(Name = "Assignment 1 Obtained Marks")]
        public Nullable<decimal> A1_Obt;
        [Display(Name = "Assignment 2 Maximum Marks")]
        public Nullable<int> A2_Max;
        [Display(Name = "Assignment 2 Obtained Marks")]
        public Nullable<decimal> A2_Obt;
        [Display(Name = "Assignment 3 Max Marks")]
        public Nullable<int> A3_Max;
        [Display(Name = "Assignment 3 Obtained Marks")]
        public Nullable<decimal> A3_Obt;
        [Display(Name = "Assignment 4 Max Marks")]
        public Nullable<int> A4_Max;
        [Display(Name = "Assignment 4 Obtained Marks")]
        public Nullable<decimal> A4_Obt;
        [Display(Name = "Assignment 5 Max Marks")]
        public Nullable<int> A5_Max;
        [Display(Name = "Assignment 5 Obtained Marks")]
        public Nullable<decimal> A5_Obt;
        [Display(Name = "Quiz 1 Max Marks")]
        public Nullable<int> Q1_Max;
        [Display(Name = "Quiz 1 Obtained Marks")]
        public Nullable<decimal> Q1_Obt;
        [Display(Name = "Quiz 2 Max Marks")]
        public Nullable<int> Q2_Max;
        [Display(Name = "Quiz 2 Obtained Marks")]
        public Nullable<decimal> Q2_Obt;
        [Display(Name = "Quiz 3 Max Marks")]
        public Nullable<int> Q3_Max;
        [Display(Name = "Quiz 3 Obtained Marks")]
        public Nullable<decimal> Q3_Obt;
        [Display(Name = "Mid Term Maximum Marks")]
        public Nullable<int> Mid_Max;
        [Display(Name = "Mid Term Obtained Marks")]
        public Nullable<decimal> Mid_Obt;
        [Display(Name = "Send Ups Maximum Marks")]
        public Nullable<int> SendUp_Max;
        [Display(Name = "Send Ups Obtained Marks")]
        public Nullable<decimal> SendUp_Obt;
        [Display(Name = "Final Maximum Marks")]
        public Nullable<int> Final_Max;
        [Display(Name = "Final Obtained Marks")]
        public Nullable<decimal> Final_Obt;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
    }

    public class BatchMetadata
    {
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public Nullable<System.DateTime> StartDt;
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public Nullable<System.DateTime> EndDt;
        [EnumDataType(typeof(Status))]
        public string Status;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
    }

    public class CourseMetadata
    {
        [StringLength(10)]
        public string Code;
        [StringLength(50)]
        public string Title;
        public string Description;
        [Display(Name = "Credit Hours")]
        public string CreaditHours;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
    }

    public class DepartmentMetadata
    {
        [StringLength(20)]
        public string Name;
        public string Description;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
    }

    public class EnrollmentMetadata
    {
        [Display(Name = "Section")]
        public Nullable<int> SectionID;
        [Display(Name = "Course")]
        public Nullable<int> CourseID;
        [Display(Name = "Batch")]
        public Nullable<int> BatchID;
        [Display(Name = "Student Name")]
        public Nullable<int> StudentID;
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> EnrollmentDate;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
    }

    public class InstructorMetadate
    {
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
        [Display(Name = "First Name")]
        [StringLength(20,ErrorMessage = "Name must be less than 20 Characters")]
        public string FirstName;
        [Display(Name = "Last Name")]
        [StringLength(20, ErrorMessage = "Name must be less than 20 Characters")]
        public string LastName;
        [Display(Name = "Father Name")]
        [StringLength(20, ErrorMessage = "Name must be less than 20 Characters")]
        public string FatherName;
        [RegularExpression("^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$",ErrorMessage = "Enter Valid CNIC")]
        public string CNIC;
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [EnumDataType(typeof(Designation))]
        public string Designation;
        [Display(Name = "Department")]
        //[EnumDataType(typeof(Department))]
        public Nullable<int> DeptID;
        public string Email;
        [Display(Name = "Administrative Position")]
        [EnumDataType(typeof(Post))]
        public string DeptPosition;
        [Display(Name = "Mobile")]
        public string MobileNo;
        [Display(Name = "Phone")]
        public string PhoneNo;
        [Display(Name = "Present Address")]
        public string PresentAddress;
        [Display(Name = "Permanent Address")]
        public string PermanentAddress;
        [Display(Name = "Present City")]
        public string PresentCity;
        [Display(Name = "Permanent City")]
        public string PermanentCity;
        [Display(Name = "Experience in Years")]
        [Range(1,20)]
        public Nullable<int> ExperienceYear;
        [Display(Name = "Experience in Months")]
        [Range(0,12)]
        public Nullable<int> ExperienceMonth;
        [Display(Name = "Joining Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> JoiningDate;
        [Display(Name = "Resignation Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ResignationDate;
        [DataType(DataType.Upload)]
        public byte[] Photo;
        [ScaffoldColumn(false)]
        public Nullable<int> ModifiedBy;
        [ScaffoldColumn(false)]
        public string Status;
        
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
    }

    public class InstructorCoursMetadata
    {        
        [Display(Name = "Section")]
        public int SectionID;
        [Display(Name = "Instructor")]
        public Nullable<int> InstructorID;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [Display(Name = "Batch")]
        public Nullable<int> BatchID { get; set; }
        [Display(Name = "Course")]
        public Nullable<int> CourseID { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [Display(Name = "User Name" )]
        public Nullable<int> uID;
    }

    public class SectionMetadata 
    {
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
    }

    public class StudentMetadata 
    {
        [ScaffoldColumn(false)]
        public Nullable<int> uID;
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName;
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName;
        [Display(Name = "Father Name")]
        [StringLength(20)]
        public string FatherName;
        [EmailAddress]
        public string Email;
        [RegularExpression("^+92-3[0-9+]{2}-[0-9+]{7}$", ErrorMessage = "Enter a Valid Mobile Number sa +92-300-0000000")]
        public string MobileNo;
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateOfBirth;
        [RegularExpression("^[0-9+]{5}-[0-9+]{7}-[0-9]{1}$", ErrorMessage = "Enter a Valid CNIC as 00000-0000000-0")]
        public string CNIC;
        public string Gender;
        [Display(Name = "Temporary Address")]
        public string TemporaryAddress;
        [Display(Name = "Temporary City")]
        public string TemporaryCity;
        [Display(Name = "Permanent Address")]
        public string PermanentAddress;
        [Display(Name = "Permanent City")]
        public string PermanentCity;
        [ScaffoldColumn(false)]
        public string Status;
        [DataType(DataType.Upload)]
        public byte[] Photo;
        [ScaffoldColumn(false)]
        public Nullable<int> ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
    }

    public class UserMetadata 
    {
        [Required(ErrorMessage="Enter Username")]
        [Display(Name = "User Name")]
        public string UserName;
        [Required(ErrorMessage = "Enter Password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UPassword;
        [ScaffoldColumn(false)]
        [EnumDataType(typeof(SessionRole))]
        public string Role;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> ModifiedOn;
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> LastAccessedOn;
        [ScaffoldColumn(false)]
        public string Status;
    }

    //Public Enumerators
    //public enum Department 
    //{
    //    ICT, 
    //    [Display(Name = "Auto & Diesel")]
    //    AD, 
    //    Mechanical, 
    //    Civil, 
    //    [Display(Name = "Quantity Surveyor")]
    //    QS, 
    //    General,
    //    Software,
    //    Databases,
    //    Networks,
    //    Marketing,
    //    [Display(Name = "Graphics Designing")]
    //    Graphics
    //}

    public enum Post
    {
        [Display(Name = "Head Of Department")]
        HOD,
        [Display(Name = "Assistant HOD")]
        AHOD,
        None

    }

    public enum Designation
    {
        [Display(Name = "Chief Instructor (CI)")]
        CI,
        Manager,
        Instructor,
        [Display(Name = "Network Administrator")]
        NetworkEngineer,
        [Display(Name = "Database Administrator (DBA)")]
        DbAdmin,
        [Display(Name = "Junior Instructor")]
        JnrInstructor,
        [Display(Name = "Lab Technician")]
        LabTechnician,
        [Display(Name = "Sub Instructor")]
        SubInstructor,
        [Display(Name = "Equipment Specialist")]
        EquipSpecialist,
        [Display(Name = "Trade Instructor")]
        TradeInstructor,
        Librarian
    }

    public enum Status {Active, Finished}
    // Enum to be used as Session String
    public enum SessionRole { Admin, Instructor, Student }
}
