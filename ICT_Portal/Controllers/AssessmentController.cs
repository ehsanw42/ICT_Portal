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
    public class AssessmentController : Controller
    {
        private ICTDBLiveEntities db = new ICTDBLiveEntities();

        // GET: /Assessment/
        public ActionResult Index(int? bid, int? cid, int? sid)
        {
            IEnumerable<Assessment> assessments = this.GetAssessments(bid, cid, sid);
            if (assessments != null)
                return View(assessments.ToList());
            return View();

            //var assessments = db.Assessments.Include(a => a.Enrollment).Include(a => a.Instructor).Include(a => a.User);
            //return View(assessments.ToList());
        }

        private IEnumerable<Assessment> GetAssessments(int? bid, int? cid, int? sid)
        {
            if (Session["utype"].ToString().ToLower() == "instructor")
            {
                int uid = int.Parse(Session["uid"].ToString());
                // Select a course for adding marks
                var r = db.InstructorCourses.Where(x => x.Instructor.User.UID == uid
                                                        && x.BatchID == bid 
                                                        && x.SectionID == sid 
                                                        && x.CourseID == cid
                                                   );

                InstructorCours instCourse = null;
                if (r.Count() > 0)
                {
                    instCourse = r.ToList()[0];
                    //instCourse.ClassRoom
                }
                // proceed only if a course is assigned to instructor
                IEnumerable<Assessment> assessments = null;
                if (instCourse != null)
                {
                    // Get enrollments for filtered batch, section and course
                    var enr = db.Enrollments.Where(x => x.InstructorCoursesID == instCourse.ID);

                    if (enr.Count() > 0)
                    {
                        assessments = db.Assessments.Where(x => enr.Any(eid => eid.ID == x.EnrollmentID));
                        return assessments.ToList();
                    }
                }
            }
            
            return null;
        }


        // GET: /Assessment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // GET: /Assessment/Create
        public ActionResult Create(int? bid, int? cid, int? sid)
        {
            IEnumerable<Assessment> assessments = this.GetAssessments(bid, cid, sid);
            if (assessments != null)
                return View(assessments.ToList());
            return View();

            //ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status");
            //ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName");
            //ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            //return View();
        }

        // POST: /Assessment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IEnumerable<Assessment> assessments, string bid, string cid, string sid)
        {
            if (ModelState.IsValid)
            {
                // Update record of each student in assessment table
                foreach (var asmnt in assessments)
                {
                    db.Entry(asmnt).State = EntityState.Modified;
                }
                // Finally update database

                //db.Assessments.Add(assessment);
                db.SaveChanges();
                return RedirectToAction("Index", routeValues: new { bid, cid, sid });
            }

            //ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", assessment.EnrollmentID);
            //ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", assessment.uID);
            //ViewBag.uID = new SelectList(db.Users, "UID", "UserName", assessment.uID);
            return View(assessments);
        }

        // GET: /Assessment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", assessment.EnrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", assessment.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", assessment.uID);
            return View(assessment);
        }

        // POST: /Assessment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,uID,EnrollmentID,A1_Max,A1_Obt,A2_Max,A2_Obt,A3_Max,A3_Obt,A4_Max,A4_Obt,A5_Max,A5_Obt,Q1_Max,Q1_Obt,Q2_Max,Q2_Obt,Q3_Max,Q3_Obt,Mid_Max,Mid_Obt,SendUp_Max,SendUp_Obt,Final_Max,Final_Obt,CreatedOn,ModifiedOn")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnrollmentID = new SelectList(db.Enrollments, "ID", "Status", assessment.EnrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", assessment.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", assessment.uID);
            return View(assessment);
        }

        // GET: /Assessment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assessment assessment = db.Assessments.Find(id);
            if (assessment == null)
            {
                return HttpNotFound();
            }
            return View(assessment);
        }

        // POST: /Assessment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assessment assessment = db.Assessments.Find(id);
            db.Assessments.Remove(assessment);
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
