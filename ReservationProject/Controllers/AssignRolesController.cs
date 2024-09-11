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
    [Authorize(Roles = "Manager")]
    public class AssignRolesController : Controller
    {
        private Model5 db = new Model5();

        // GET: AssignRoles
        public ActionResult Index()
        {
            var assignRoles = db.AssignRoles.Include(a => a.Admin).Include(a => a.RoleAdm);
            return View(assignRoles.ToList());
        }

        // GET: AssignRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRole assignRole = db.AssignRoles.Find(id);
            if (assignRole == null)
            {
                return HttpNotFound();
            }
            return View(assignRole);
        }

        // GET: AssignRoles/Create
        public ActionResult Create()
        {
            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME");
            ViewBag.ROLE_FID = new SelectList(db.RoleAdms, "ROLEADMIN_ID", "AD_ROLE_NAME");
            return View();
        }

        // POST: AssignRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ASSIGN_ROLE_ID,ADMIN_FID,ROLE_FID")] AssignRole assignRole)
        {
            if (ModelState.IsValid)
            {
                db.AssignRoles.Add(assignRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", assignRole.ADMIN_FID);
            ViewBag.ROLE_FID = new SelectList(db.RoleAdms, "ROLEADMIN_ID", "AD_ROLE_NAME", assignRole.ROLE_FID);
            return View(assignRole);
        }

        // GET: AssignRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRole assignRole = db.AssignRoles.Find(id);
            if (assignRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", assignRole.ADMIN_FID);
            ViewBag.ROLE_FID = new SelectList(db.RoleAdms, "ROLEADMIN_ID", "AD_ROLE_NAME", assignRole.ROLE_FID);
            return View(assignRole);
        }

        // POST: AssignRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ASSIGN_ROLE_ID,ADMIN_FID,ROLE_FID")] AssignRole assignRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", assignRole.ADMIN_FID);
            ViewBag.ROLE_FID = new SelectList(db.RoleAdms, "ROLEADMIN_ID", "AD_ROLE_NAME", assignRole.ROLE_FID);
            return View(assignRole);
        }

        // GET: AssignRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignRole assignRole = db.AssignRoles.Find(id);
            if (assignRole == null)
            {
                return HttpNotFound();
            }
            return View(assignRole);
        }

        // POST: AssignRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignRole assignRole = db.AssignRoles.Find(id);
            db.AssignRoles.Remove(assignRole);
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
