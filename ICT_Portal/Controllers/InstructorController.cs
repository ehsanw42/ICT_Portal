using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ICT_Portal.Models;

namespace ICT_Portal.Controllers
{
    public class InstructorController : Controller
    {
        private ICTDBLiveEntities db = new ICTDBLiveEntities();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check Session
            if (Session["utype"] != null)
            {
                base.OnActionExecuting(filterContext);
            }
            else
            {
                // Redirect to Login page if session is null
                filterContext.Result = new RedirectResult("~/User/Login");
            }
        }


        // GET: /Instructor/
        public ActionResult Index()
        {
            if (Session["utype"].ToString().ToLower() == "instructor")
            {
                return RedirectToAction("Details");
            }
            var instructors = db.Instructors.Include(i => i.Department).Include(i => i.User).Include(i => i.User1);
            return View(instructors.ToList());
        }

        // GET: /Instructor/Details/5
        public ActionResult Details(int? id)
        {
            Instructor instructor = null;
            if (Session["utype"].ToString().ToLower() == "instructor"){
                int uid = int.Parse(Session["uid"].ToString());
                instructor = db.Instructors.FirstOrDefault(x => x.uID == uid);
                if (instructor != null)
                    return View(instructor);
            }
            else if (Session["utype"].ToString().ToLower() == "admin")
            {
                instructor = db.Instructors.Find(id);
                if (instructor != null)
                    return View(instructor);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: /Instructor/Create
        public ActionResult Create()
        {
            ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            ViewBag.ModifiedBy = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /Instructor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,uID,FirstName,LastName,FatherName,CNIC,DateOfBirth,Gender,Designation,DeptID,Email,DeptPosition,MobileNo,PhoneNo,PresentAddress,PermanentAddress,PresentCity,PermanentCity,ExperienceYear,ExperienceMonth,JoiningDate,ResignationDate,Photo,Status,CreatedOn,ModifiedBy,ModifiedOn")] Instructor instructor, HttpPostedFileBase image)
        public ActionResult Create(Instructor instructor, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                User usr = new User();
                usr.UserName = instructor.Username;
                usr.UPassword = instructor.Password;
                usr.Role = SessionRole.Instructor.ToString();
                usr.Status = Status.Active.ToString();               
                db.Users.Add(usr);
                db.SaveChanges();

                instructor.uID = usr.UID;
                if (image != null)
                {
                    instructor.Photo = new byte[image.ContentLength];
                    image.InputStream.Read(instructor.Photo, 0, image.ContentLength);
                }
                db.Instructors.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name", instructor.DeptID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructor.uID);
            ViewBag.ModifiedBy = new SelectList(db.Users, "UID", "UserName", instructor.ModifiedBy);
            return View(instructor);
        }

        // GET: /Instructor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Instructor ins = null;
            if (Session["utype"].ToString().ToLower() == "instructor")
            {
                int uid = int.Parse(Session["uid"].ToString());
                ins = db.Instructors.FirstOrDefault(x => x.uID == uid);
                if (ins != null)
                {
                    ViewBag.uID = new SelectList(db.Users, "UID", "UserName", ins.uID);
                    ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name", ins.DeptID);
                    ViewBag.ModifiedBy = new SelectList(db.Users, "UID", "UserName", ins.ModifiedBy);
                    ins.Username = ins.User.UserName;
                    ins.Password = ins.User.UPassword;
                    return View(ins);
                }
            }
            else if (Session["utype"].ToString().ToLower() == "admin")
            {
                ins = db.Instructors.Find(id);
                if (ins != null)
                {
                    ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name", ins.DeptID);
                    ViewBag.uID = new SelectList(db.Users, "UID", "UserName", ins.uID);
                    ViewBag.ModifyBy = new SelectList(db.Users, "UID" , "UserName" , ins.ModifiedBy);
                    return View(ins);
                }
                else if (ins == null)
                {
                    return HttpNotFound();
                }
            }

            Instructor instructor = db.Instructors.Find(id);
            instructor.Username = instructor.User.UserName;
            instructor.Password = instructor.User.UPassword;
            return View(instructor);
        }

        // POST: /Instructor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include="ID,uID,FirstName,LastName,FatherName,CNIC,DateOfBirth,Gender,Designation,DeptID,Email,DeptPosition,MobileNo,PhoneNo,PresentAddress,PermanentAddress,PresentCity,PermanentCity,ExperienceYear,ExperienceMonth,JoiningDate,ResignationDate,Photo,Status,CreatedOn,ModifiedBy,ModifiedOn")] Instructor instructor, HttpPostedFileBase image)
        public ActionResult Edit(Instructor instructor, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    instructor.Photo = new byte[image.ContentLength];
                    image.InputStream.Read(instructor.Photo, 0, image.ContentLength);
                }
                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details");
            }
            ViewBag.DeptID = new SelectList(db.Departments, "ID", "Name", instructor.DeptID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructor.uID);
            ViewBag.ModifiedBy = new SelectList(db.Users, "UID", "UserName", instructor.ModifiedBy);
            return View(instructor);
        }

        // GET: /Instructor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructors.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: /Instructor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructors.Find(id);
            db.Instructors.Remove(instructor);
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
