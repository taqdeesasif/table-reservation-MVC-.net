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
    public class ReservationsController : Controller
    {
        private Model5 db = new Model5();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Customer).Include(r => r.TabCategory);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.CUSTOMER_FID = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME");
            ViewBag.TABCATEGORY_FID = new SelectList(db.TabCategories, "TABCATEGORY_ID", "TABCATEGORY_NAME");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RESERVATION_ID,TABCATEGORY_FID,DATE_TIME,CHECKIN_DATE,RESERVATION_STATUS,STATUS,ORDER_STATUS,CHECKIN_TIME,ADULTS,KIDS,SPECIAL_REQUEST,TOTAL_BILL,WITH_PREORDER,CUSTOMER_NAME,CUSTOMER_EMAIL,CUSTOMER_CONTACT,CUSTOMER_ADDRESS,CUSTOMER_FID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CUSTOMER_FID = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME", reservation.CUSTOMER_FID);
            ViewBag.TABCATEGORY_FID = new SelectList(db.TabCategories, "TABCATEGORY_ID", "TABCATEGORY_NAME", reservation.TABCATEGORY_FID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CUSTOMER_FID = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME", reservation.CUSTOMER_FID);
            ViewBag.TABCATEGORY_FID = new SelectList(db.TabCategories, "TABCATEGORY_ID", "TABCATEGORY_NAME", reservation.TABCATEGORY_FID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RESERVATION_ID,TABCATEGORY_FID,DATE_TIME,CHECKIN_DATE,RESERVATION_STATUS,STATUS,ORDER_STATUS,CHECKIN_TIME,ADULTS,KIDS,SPECIAL_REQUEST,TOTAL_BILL,WITH_PREORDER,CUSTOMER_NAME,CUSTOMER_EMAIL,CUSTOMER_CONTACT,CUSTOMER_ADDRESS,CUSTOMER_FID")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CUSTOMER_FID = new SelectList(db.Customers, "CUSTOMER_ID", "CUSTOMER_NAME", reservation.CUSTOMER_FID);
            ViewBag.TABCATEGORY_FID = new SelectList(db.TabCategories, "TABCATEGORY_ID", "TABCATEGORY_NAME", reservation.TABCATEGORY_FID);
            return View(reservation);
        }


        public ActionResult OrderServed(int id)
        {
            Reservation o = db.Reservations.Where(x => x.RESERVATION_ID == id).FirstOrDefault();
            o.ORDER_STATUS = "Served";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DailyReport","Reports");
        }






        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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
