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
    public class AvailibilitiesController : Controller
    {
        private Model5 db = new Model5();

        // GET: Availibilities
        public ActionResult Index()
        {
            return View(db.Availibilities.ToList());
        }

        // GET: Availibilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availibility availibility = db.Availibilities.Find(id);
            if (availibility == null)
            {
                return HttpNotFound();
            }
            return View(availibility);
        }

        // GET: Availibilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Availibilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AVAILABLE_ID,UNAVAILABLE_DATE,UNAVAILABLE_TIME")] Availibility availibility)
        {
            if (ModelState.IsValid)
            {
                db.Availibilities.Add(availibility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(availibility);
        }

        // GET: Availibilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availibility availibility = db.Availibilities.Find(id);
            if (availibility == null)
            {
                return HttpNotFound();
            }
            return View(availibility);
        }

        // POST: Availibilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AVAILABLE_ID,UNAVAILABLE_DATE,UNAVAILABLE_TIME")] Availibility availibility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availibility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(availibility);
        }

        // GET: Availibilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availibility availibility = db.Availibilities.Find(id);
            if (availibility == null)
            {
                return HttpNotFound();
            }
            return View(availibility);
        }

        // POST: Availibilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Availibility availibility = db.Availibilities.Find(id);
            db.Availibilities.Remove(availibility);
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
