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
    public class EnrollmentController : Controller
    {
        private ICTDBLiveEntities db = new ICTDBLiveEntities();

        // GET: /Enrollment/
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.InstructorCours).Include(e => e.Student).Include(e => e.User);
            return View(enrollments.ToList());
        }

        // GET: /Enrollment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: /Enrollment/Create
        public ActionResult Create()
        {
            ViewBag.InstructorCoursesID = new SelectList(db.InstructorCourses, "ID", "ClassRoom");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,StudentID,InstructorCoursesID,EnrollmentDate,CreatedOn,ModifiedOn,Status,uID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorCoursesID = new SelectList(db.InstructorCourses, "ID", "ClassRoom", enrollment.InstructorCoursesID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName", enrollment.StudentID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", enrollment.uID);
            return View(enrollment);
        }

        // GET: /Enrollment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorCoursesID = new SelectList(db.InstructorCourses, "ID", "ClassRoom", enrollment.InstructorCoursesID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName", enrollment.StudentID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", enrollment.uID);
            return View(enrollment);
        }

        // POST: /Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,StudentID,InstructorCoursesID,EnrollmentDate,CreatedOn,ModifiedOn,Status,uID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstructorCoursesID = new SelectList(db.InstructorCourses, "ID", "ClassRoom", enrollment.InstructorCoursesID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "FirstName", enrollment.StudentID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", enrollment.uID);
            return View(enrollment);
        }

        // GET: /Enrollment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: /Enrollment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
