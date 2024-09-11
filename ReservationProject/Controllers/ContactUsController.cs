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
    [Authorize(Roles = "Manager,Operations_Manager")]
    public class ContactUsController : Controller
    {
        private Model5 db = new Model5();

        // GET: ContactUs
        public ActionResult Index()
        {
            var contactUs = db.ContactUs.Include(c => c.Customer);
            return View(contactUs.ToList());
        }

        // GET: ContactUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactU contactU = db.ContactUs.Find(id);
            if (contactU == null)
            {
                return HttpNotFound();
            }
            return View(contactU);
        }

        // GET: ContactUs/Create
        public ActionResult Create()
        {
            ViewBag.CUSTOMER_FIDDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME");
            return View();
        }

        // POST: ContactUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CONTACTUS_ID,CUSTOMER_FIDDD,CONTACTUS_EMAIL,CONTACTUS_MESSAGE,CONTACTUS_DATE")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(contactU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CUSTOMER_FIDDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME", contactU.CUSTOMER_FIDDD);
            return View(contactU);
        }

        // GET: ContactUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactU contactU = db.ContactUs.Find(id);
            if (contactU == null)
            {
                return HttpNotFound();
            }
            ViewBag.CUSTOMER_FIDDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME", contactU.CUSTOMER_FIDDD);
            return View(contactU);
        }

        // POST: ContactUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONTACTUS_ID,CUSTOMER_FIDDD,CONTACTUS_EMAIL,CONTACTUS_MESSAGE,CONTACTUS_DATE")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CUSTOMER_FIDDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME", contactU.CUSTOMER_FIDDD);
            return View(contactU);
        }

        // GET: ContactUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactU contactU = db.ContactUs.Find(id);
            if (contactU == null)
            {
                return HttpNotFound();
            }
            return View(contactU);
        }

        // POST: ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactU contactU = db.ContactUs.Find(id);
            db.ContactUs.Remove(contactU);
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
