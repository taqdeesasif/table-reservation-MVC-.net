using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservationProject.Models;
using System.Web.UI.WebControls;

using System.Net.Mail;

namespace ReservationProject.Controllers
{
    public class CustomersController : Controller
    {
        private Model5 db = new Model5();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CUSTOMER_ID,CUSTOMER_NAME,CUSTOMEREMAIL,CUSTOMERADDRESS,CUSTOMERCONTACT,CUSTOMER_PASSWORD")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CUSTOMER_ID,CUSTOMER_NAME,CUSTOMEREMAIL,CUSTOMERADDRESS,CUSTOMERCONTACT,CUSTOMER_PASSWORD")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Customer customer)
        {
            Customer c = db.Customers.Where(x => x.CUSTOMEREMAIL == customer.CUSTOMEREMAIL && x.CUSTOMER_PASSWORD == customer.CUSTOMER_PASSWORD).FirstOrDefault();
            if (c != null)
            {
               

                Session["Customer"] = c;
                return RedirectToAction("DisplayTables", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();

            }


        }

        public ActionResult Logout()
        {
           
            Session["Customer"] = null;

            return RedirectToAction("IndexCustomer","Home");
        }

        public ActionResult CustomerHistory()
        {
            return View();
           
        }

        public ActionResult Invoice(int id)
        {
            var o = db.Reservations.Where(x => x.RESERVATION_ID == id).ToList();
            return View(o);
        }


       


        public ActionResult CancelledOrder()
        {
           
            return View();
        }

        public ActionResult OrderCancellation(int id)
        {
            Reservation o = db.Reservations.Where(x => x.RESERVATION_ID == id).FirstOrDefault();
            o.STATUS = "Cancelled";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CustomerHistory");
        }

        public ActionResult OrderActivate(int id)
        {
            Reservation o = db.Reservations.Where(x => x.RESERVATION_ID == id).FirstOrDefault();
            o.STATUS = "Active";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CancelledOrder");
        }

        public ActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPass(Customer customer)
        {
            Customer c = db.Customers.Where(x => x.CUSTOMEREMAIL == customer.CUSTOMEREMAIL).FirstOrDefault();
            if (c != null)
            {
                //Email msg

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("foodoclockr@gmail.com");
                mail.To.Add(customer.CUSTOMEREMAIL);
                mail.Subject = "Forget Password!! FoodO'clock";
                mail.Body = "<b>Food O'clock!</b> <br/> Dear " +c.CUSTOMER_NAME+ "<br/>Your Account Password Is " +c.CUSTOMER_PASSWORD+ "<br/> Have a nice day...";
                mail.IsBodyHtml = true;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("foodoclockr@gmail.com",                               "Pakistan_123");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);





                return RedirectToAction("Login", "Customers");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();

            }


        }




    }
}
