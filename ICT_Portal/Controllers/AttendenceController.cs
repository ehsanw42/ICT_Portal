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
    public class AttendenceController : Controller
    {
        private ICTDBLiveEntities db = new ICTDBLiveEntities();

        // GET: /Attendence/
        public ActionResult Index()
        {
            int uid = int.Parse(Session["uid"].ToString());
            var attendences = db.Attendences
                .Include(a => a.Enrollment)
                .Include(a => a.Instructor)
                .Include(a => a.User)
                .Where(m => m.uID == uid);
            return View(attendences.ToList());
        }

        // GET: /Attendence/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: /Attendence/Create
        public ActionResult Create()
        {
            ViewBag.CoursesList = new SelectList((from InstructorCourses in db.InstructorCourses
                                                  where
                                                    InstructorCourses.Instructor.uID == int.Parse(Session["uid"].ToString())
                                                    &&
                                                    InstructorCourses.Batch.Status == "Active"
                                                  select new
                                                  {
                                                      InstructorCourses.Course.ID,
                                                      InstructorCourses.Course.Code,
                                                      InstructorCourses.Course.Title,
                                                      InstructorCourses.Course.Description,
                                                      InstructorCourses.Course.CreaditHours,
                                                      CreatedOn = (DateTime?)InstructorCourses.Course.CreatedOn,
                                                      ModifiedOn = (DateTime?)InstructorCourses.Course.ModifiedOn,
                                                      uID = (int?)InstructorCourses.Course.uID
                                                  }), "ID", "Title");
            ViewBag.SectionList = new SelectList((from InstructorCourses in db.InstructorCourses
                                                  where InstructorCourses.Instructor.uID == int.Parse(Session["uid"].ToString()) 
                                                  && InstructorCourses.Batch.Status == "Active"
                                                  select new
                                                  {
                                                      InstructorCourses.Section.ID,
                                                      InstructorCourses.Section.Name,
                                                      ModifiedOn = (DateTime?)InstructorCourses.Section.ModifiedOn,
                                                      CreatedOn = (DateTime?)InstructorCourses.Section.CreatedOn,
                                                      uID = (int?)InstructorCourses.Section.uID
                                                  }).Distinct(), "ID", "Name");
            
            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status");
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /Attendence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Attendence attendence)
        {
            int SelectedCourse = int.Parse(Request.Form["Courselist"]);
            int SelectedSection = int.Parse(Request.Form["Sectionlist"]);
            if (ModelState.IsValid)
            {
                db.Attendences.Add(attendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.EnrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", attendence.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", attendence.uID);
            return View(attendence);
        }

        // GET: /Attendence/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.EnrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", attendence.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", attendence.uID);
            return View(attendence);
        }

        // POST: /Attendence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,EnrollmentID,uID,ClassRoom,Status,EntryDate,FromTime,ToTime,ModifiedOn,CreatedOn,TopicsCovered")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.EnrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", attendence.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", attendence.uID);
            return View(attendence);
        }

        // GET: /Attendence/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendence attendence = db.Attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: /Attendence/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendence attendence = db.Attendences.Find(id);
            db.Attendences.Remove(attendence);
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
