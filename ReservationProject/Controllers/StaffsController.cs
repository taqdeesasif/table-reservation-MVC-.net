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
    public class StaffsController : Controller
    {
        private Model5 db = new Model5();

        // GET: Staffs
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.StaffJob);
            return View(staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            ViewBag.STAFFJOB_FID = new SelectList(db.StaffJobs, "STAFFJOB_ID", "STAFFJOB_NAME");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Staff staff)
        {
            if (ModelState.IsValid)
            {
                staff.STAFF_PIC.SaveAs(Server.MapPath("~/StaffPicture/" + staff.STAFF_PIC.FileName));
                staff.STAFF_PICTURE = "~/StaffPicture/" + staff.STAFF_PIC.FileName;




                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.STAFFJOB_FID = new SelectList(db.StaffJobs, "STAFFJOB_ID", "STAFFJOB_NAME", staff.STAFFJOB_FID);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
           
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
           
            ViewBag.STAFFJOB_FID = new SelectList(db.StaffJobs, "STAFFJOB_ID", "STAFFJOB_NAME", staff.STAFFJOB_FID);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Staff staff)
        {
            if (ModelState.IsValid)
            {
                if (staff.STAFF_PIC != null)
                {

                    staff.STAFF_PIC.SaveAs(Server.MapPath("~/StaffPicture/" + staff.STAFF_PIC.FileName));
                    staff.STAFF_PICTURE = "~/StaffPicture/" + staff.STAFF_PIC.FileName;



                }

                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.STAFFJOB_FID = new SelectList(db.StaffJobs, "STAFFJOB_ID", "STAFFJOB_NAME", staff.STAFFJOB_FID);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
