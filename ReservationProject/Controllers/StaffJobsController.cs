using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservationProject.Models;

namespace ReservationProject.Controllers
{
    [Authorize(Roles = "Manager,Data_Clerk")]
    public class StaffJobsController : Controller
    {
        private Model5 db = new Model5();

        // GET: StaffJobs
        public ActionResult Index()
        {
            return View(db.StaffJobs.ToList());
        }

        // GET: StaffJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffJob staffJob = db.StaffJobs.Find(id);
            if (staffJob == null)
            {
                return HttpNotFound();
            }
            return View(staffJob);
        }

        // GET: StaffJobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STAFFJOB_ID,STAFFJOB_NAME")] StaffJob staffJob)
        {
            if (ModelState.IsValid)
            {
                db.StaffJobs.Add(staffJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffJob);
        }

        // GET: StaffJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffJob staffJob = db.StaffJobs.Find(id);
            if (staffJob == null)
            {
                return HttpNotFound();
            }
            return View(staffJob);
        }

        // POST: StaffJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STAFFJOB_ID,STAFFJOB_NAME")] StaffJob staffJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffJob);
        }

        // GET: StaffJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffJob staffJob = db.StaffJobs.Find(id);
            if (staffJob == null)
            {
                return HttpNotFound();
            }
            return View(staffJob);
        }

        // POST: StaffJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffJob staffJob = db.StaffJobs.Find(id);
            db.StaffJobs.Remove(staffJob);
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
