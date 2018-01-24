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
    public class UserController : Controller
    {
        private ICTDBLiveEntities db = new ICTDBLiveEntities();

        // GET: /User/
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: /User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //GET: /User/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User usr = db.Users.FirstOrDefault(x => x.UPassword == user.UPassword && x.UserName.ToLower() == user.UserName.ToLower());
                if (usr == null)
                {
                    ModelState.AddModelError("","Invalid Username or Password");                    
                    return View();
                }

                Session["uid"] = usr.UID;
                Session["utype"] = usr.Role;
                if(usr.Role == "Admin")
                {
                    Session["username"] = usr.UserName;
                    //return Redirect(returnUrl);
                    return RedirectToAction("Index", "User");
                }
                else if (usr.Role == "Instructor")
                {
                    Session["username"] = db.Instructors.FirstOrDefault(x => x.uID == usr.UID).FirstName;
                    //return Redirect(returnUrl);
                    return RedirectToAction("Details", "Instructor");
                }
                else if (usr.Role == "Student")
                {
                    Session["username"] = db.Students.FirstOrDefault(x => x.uID == usr.UID).FirstName;
                    //return Redirect(returnUrl);
                    return RedirectToAction("Index", "Student");
                }
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Logoff()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UID,UserName,UPassword,Role,CreatedOn,ModifiedOn,LastAccessedOn,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: /User/Edit/5
        public ActionResult Edit()
        {
            if (Session["utype"] != null)
            {
                int uid = int.Parse(Session["uid"].ToString());
                User obj = db.Users.Where(x => x.UID == uid).SingleOrDefault();
                return View(obj);
            }
            return View("Login");
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UID,UserName,UPassword,RePassword,RePassword2")] User user)
        {
            if (ModelState.IsValid)
            {
                //Update Password if Current and New Password are Same
                User obj = db.Users.Where(x => x.UPassword == user.UPassword).SingleOrDefault();

                //Do Not Update Database id Current and New Password are Same
                if (obj != null)
                {
                    if (obj.UPassword != user.RePassword)
                    {
                        obj.UPassword = user.RePassword;
                        db.SaveChanges();
                    }
                    ViewBag.Success = "Password Updated Successfully";
                    return View(user);
                }
                ViewBag.Error = "Current Pasword is Incorrect";                
            }
            return View(user);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
