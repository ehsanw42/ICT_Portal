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
    public class InstructorCoursController : Controller
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


        // GET: /InstructorCours/
        public ActionResult Index()
        {
            if (Session["utype"].ToString().ToLower() == "instructor")
            {
                int uid = int.Parse(Session["uid"].ToString());
                var instructorcourses = db.InstructorCourses
                    .Include(i => i.Batch)
                    .Include(i => i.Course)
                    .Include(i => i.Instructor)
                    .Include(i => i.Section)
                    .Include(i => i.User)
                    .Where(m => m.uID == uid && m.Batch.Status.ToLower() == "active");
                //Session["batch"] = instructorcourses.SingleOrDefault().BatchID;
                //Session["section"] = instructorcourses.SingleOrDefault().SectionID;
                //Session["course"] = instructorcourses.SingleOrDefault().CourseID;
                return View(instructorcourses.ToList());

            }
            else if (Session["utype"].ToString().ToLower() == "admin")
            {
                var instructorcourses = db.InstructorCourses
                    .Include(i => i.Batch)
                    .Include(i => i.Course)
                    .Include(i => i.Instructor)
                    .Include(i => i.Section)
                    .Include(i => i.User);
                return View(instructorcourses.ToList());
            }
            return RedirectToAction("Login", "User");
        }




        // GET: /InstructorCours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstructorCours instructorcours = db.InstructorCourses.Find(id);
            if (instructorcours == null)
            {
                return HttpNotFound();
            }
            return View(instructorcours);
        }

        // GET: /InstructorCours/Create
        public ActionResult Create()
        {
            ViewBag.BatchID = new SelectList(db.Batches, "ID", "Name");
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Code");
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FirstName");
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /InstructorCours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ClassRoom,SectionID,InstructorID,CourseID,BatchID,CreatedOn,ModifiedOn,Room,uID")] InstructorCours instructorcours)
        {
            if (ModelState.IsValid)
            {
                db.InstructorCourses.Add(instructorcours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BatchID = new SelectList(db.Batches, "ID", "Name", instructorcours.BatchID);
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Code", instructorcours.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FirstName", instructorcours.InstructorID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", instructorcours.SectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructorcours.uID);
            return View(instructorcours);
        }

        // GET: /InstructorCours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstructorCours instructorcours = db.InstructorCourses.Find(id);
            if (instructorcours == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatchID = new SelectList(db.Batches, "ID", "Name", instructorcours.BatchID);
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Code", instructorcours.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FirstName", instructorcours.InstructorID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", instructorcours.SectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructorcours.uID);
            return View(instructorcours);
        }

        // POST: /InstructorCours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClassRoom,SectionID,InstructorID,CourseID,BatchID,CreatedOn,ModifiedOn,Room,uID")] InstructorCours instructorcours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructorcours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchID = new SelectList(db.Batches, "ID", "Name", instructorcours.BatchID);
            ViewBag.CourseID = new SelectList(db.Courses, "ID", "Code", instructorcours.CourseID);
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FirstName", instructorcours.InstructorID);
            ViewBag.SectionID = new SelectList(db.Sections, "ID", "Name", instructorcours.SectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructorcours.uID);
            return View(instructorcours);
        }

        // GET: /InstructorCours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstructorCours instructorcours = db.InstructorCourses.Find(id);
            if (instructorcours == null)
            {
                return HttpNotFound();
            }
            return View(instructorcours);
        }

        // POST: /InstructorCours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstructorCours instructorcours = db.InstructorCourses.Find(id);
            db.InstructorCourses.Remove(instructorcours);
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
