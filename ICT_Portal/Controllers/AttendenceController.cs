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

        // GET: /Attendence/
        public ActionResult Index()
        {
            if (Session["utype"].ToString().ToLower() == "instructor")
            {
                return RedirectToAction("Details");
            }
            var attendences = db.Attendences.Include(a => a.Enrollment).Include(a => a.Instructor).Include(a => a.User);
            return View(attendences.ToList());
        }

        // GET: /Attendence/Details/5
        public ActionResult Details(int? id)
        {
            Attendence attendence = null;
            if (Session["utype"].ToString().ToLower() == "instructor")
            {
                int uid = int.Parse(Session["uid"].ToString());
                attendence = db.Attendences.FirstOrDefault(x => x.uID == uid);
                    // db.Instructors.FirstOrDefault(x => x.uID == uid);
                if (attendence != null)
                    return View(attendence);
            }
            else if (Session["utype"].ToString().ToLower() == "admin")
            {
                attendence = db.Attendences.Find(id);;
                if (attendence!= null)
                    return View(attendence);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: /Attendence/Create
        public ActionResult Create()
        {
            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status");
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /Attendence/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,enrollmentID,uID,ClassRoom,Status,EntryDate,FromTime,ToTime,ModifiedOn,CreatedOn,TopicsCovered")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Attendences.Add(attendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.enrollmentID);
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
            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.enrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", attendence.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", attendence.uID);
            return View(attendence);
        }

        // POST: /Attendence/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,enrollmentID,uID,ClassRoom,Status,EntryDate,FromTime,ToTime,ModifiedOn,CreatedOn,TopicsCovered")] Attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status", attendence.enrollmentID);
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
