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
            var enrollments = db.Enrollments.Include(e => e.Batch).Include(e => e.Course).Include(e => e.Section).Include(e => e.User).Include(e => e.Student);
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
            ViewBag.batchID = new SelectList(db.Batches, "ID", "Name");
            ViewBag.courseID = new SelectList(db.Courses, "ID", "Code");
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            ViewBag.sectionID = new SelectList(db.Students, "ID", "FirstName");
            return View();
        }

        // POST: /Enrollment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,sectionID,courseID,batchID,studentID,EnrollmentDate,CreatedOn,ModifiedOn,Status,uID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.batchID = new SelectList(db.Batches, "ID", "Name", enrollment.batchID);
            ViewBag.courseID = new SelectList(db.Courses, "ID", "Code", enrollment.courseID);
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name", enrollment.sectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", enrollment.uID);
            ViewBag.sectionID = new SelectList(db.Students, "ID", "FirstName", enrollment.sectionID);
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
            ViewBag.batchID = new SelectList(db.Batches, "ID", "Name", enrollment.batchID);
            ViewBag.courseID = new SelectList(db.Courses, "ID", "Code", enrollment.courseID);
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name", enrollment.sectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", enrollment.uID);
            ViewBag.sectionID = new SelectList(db.Students, "ID", "FirstName", enrollment.sectionID);
            return View(enrollment);
        }

        // POST: /Enrollment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,sectionID,courseID,batchID,studentID,EnrollmentDate,CreatedOn,ModifiedOn,Status,uID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.batchID = new SelectList(db.Batches, "ID", "Name", enrollment.batchID);
            ViewBag.courseID = new SelectList(db.Courses, "ID", "Code", enrollment.courseID);
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name", enrollment.sectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", enrollment.uID);
            ViewBag.sectionID = new SelectList(db.Students, "ID", "FirstName", enrollment.sectionID);
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
