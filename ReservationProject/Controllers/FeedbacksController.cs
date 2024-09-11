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
    public class FeedbacksController : Controller
    {
        private Model5 db = new Model5();

        // GET: Feedbacks
        public ActionResult Index()
        {
            var feedbacks = db.Feedbacks.Include(f => f.Admin).Include(f => f.Customer);
            return View(feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.ADMIN_FIDD = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME");
            ViewBag.CUSTOMER_FIDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMEREMAIL");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FEEDBACK_ID,REVIEW_TYPE,FEEDBACK_MESSAGE,FEEDBACK_EMAIL,FEEDBACK_CONTACT,FEDDBACK_DATE,CUSTOMER_FIDD,REPLY_DATE,RECEIVER_MESSAGE,ADMIN_FIDD")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.FEDDBACK_DATE = DateTime.Now;
                Admin a = (Admin)Session["Admin"];
                feedback.ADMIN_FIDD = a.ADMIN_ID;

                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ADMIN_FIDD = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", feedback.ADMIN_FIDD);
            ViewBag.CUSTOMER_FIDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMEREMAIL", feedback.CUSTOMER_FIDD);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }

            ViewBag.ADMIN_FIDD = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", feedback.ADMIN_FIDD);
            ViewBag.CUSTOMER_FIDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMEREMAIL", feedback.CUSTOMER_FIDD);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.REPLY_DATE = DateTime.Now;
                Admin a = (Admin)Session["Admin"];
                feedback.ADMIN_FIDD = a.ADMIN_ID;


                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ADMIN_FIDD = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", feedback.ADMIN_FIDD);
            ViewBag.CUSTOMER_FIDD = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMEREMAIL", feedback.CUSTOMER_FIDD);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
