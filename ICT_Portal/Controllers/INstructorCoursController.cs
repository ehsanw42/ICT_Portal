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

        // GET: /INstructorCours/
        public ActionResult Index()
        {
            var instructorcourses = db.InstructorCourses.Include(i => i.Instructor).Include(i => i.Section).Include(i => i.User);
            return View(instructorcourses.ToList());
        }

        // GET: /INstructorCours/Details/5
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

        // GET: /INstructorCours/Create
        public ActionResult Create()
        {
            ViewBag.instructorID = new SelectList(db.Instructors, "ID", "FirstName");
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /INstructorCours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,sectionID,instructorID,CreatedOn,ModifiedOn,uID")] InstructorCours instructorcours)
        {
            if (ModelState.IsValid)
            {
                db.InstructorCourses.Add(instructorcours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.instructorID = new SelectList(db.Instructors, "ID", "FirstName", instructorcours.instructorID);
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name", instructorcours.sectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructorcours.uID);
            return View(instructorcours);
        }

        // GET: /INstructorCours/Edit/5
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
            ViewBag.instructorID = new SelectList(db.Instructors, "ID", "FirstName", instructorcours.instructorID);
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name", instructorcours.sectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructorcours.uID);
            return View(instructorcours);
        }

        // POST: /INstructorCours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,sectionID,instructorID,CreatedOn,ModifiedOn,uID")] InstructorCours instructorcours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructorcours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.instructorID = new SelectList(db.Instructors, "ID", "FirstName", instructorcours.instructorID);
            ViewBag.sectionID = new SelectList(db.Sections, "ID", "Name", instructorcours.sectionID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", instructorcours.uID);
            return View(instructorcours);
        }

        // GET: /INstructorCours/Delete/5
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

        // POST: /INstructorCours/Delete/5
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
