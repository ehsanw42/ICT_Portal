﻿using System;
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
        public ActionResult Index()
        {
            var assessments = db.Assessments.Include(a => a.Enrollment).Include(a => a.Instructor).Include(a => a.User);
            return View(assessments.ToList());
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
        public ActionResult Create()
        {
            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status");
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName");
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName");
            return View();
        }

        // POST: /Assessment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,uID,enrollmentID,A1_Max,A1_Obt,A2_Max,A2_Obt,A3_Max,A3_Obt,A4_Max,A4_Obt,A5_Max,A5_Obt,Q1_Max,Q1_Obt,Q2_Max,Q2_Obt,Q3_Max,Q3_Obt,Mid_Max,Mid_Obt,SendUp_Max,SendUp_Obt,Final_Max,Final_Obt,CreatedOn,ModifiedOn")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Assessments.Add(assessment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status", assessment.enrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", assessment.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", assessment.uID);
            return View(assessment);
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
            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status", assessment.enrollmentID);
            ViewBag.uID = new SelectList(db.Instructors, "ID", "FirstName", assessment.uID);
            ViewBag.uID = new SelectList(db.Users, "UID", "UserName", assessment.uID);
            return View(assessment);
        }

        // POST: /Assessment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,uID,enrollmentID,A1_Max,A1_Obt,A2_Max,A2_Obt,A3_Max,A3_Obt,A4_Max,A4_Obt,A5_Max,A5_Obt,Q1_Max,Q1_Obt,Q2_Max,Q2_Obt,Q3_Max,Q3_Obt,Mid_Max,Mid_Obt,SendUp_Max,SendUp_Obt,Final_Max,Final_Obt,CreatedOn,ModifiedOn")] Assessment assessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.enrollmentID = new SelectList(db.Enrollments, "ID", "Status", assessment.enrollmentID);
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
