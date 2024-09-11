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
  
    public class BlogsController : Controller
    {
        private Model5 db = new Model5();

        // GET: Blogs
        public ActionResult Index()
        {
            var blogs = db.Blogs.Include(b => b.Admin);
            return View(blogs.ToList());
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.BLOG_DATE = DateTime.Now;
                Admin a = (Admin)Session["Admin"];
                blog.ADMIN_FID = a.ADMIN_ID;

                blog.BLOG_PIC.SaveAs(Server.MapPath("~/BlogPicture/" + blog.BLOG_PIC.FileName));
                blog.BLOG_PICTURE = "~/BlogPicture/" + blog.BLOG_PIC.FileName;


                db.Blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", blog.ADMIN_FID);
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
           
            Blog blog = db.Blogs.Find(id);
           
            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", blog.ADMIN_FID);
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (blog.BLOG_PIC != null)
                {
                    blog.BLOG_PIC.SaveAs(Server.MapPath("~/BlogPicture/" + blog.BLOG_PIC.FileName));
                    blog.BLOG_PICTURE = "~/BlogPicture/" + blog.BLOG_PIC.FileName;
                }


                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ADMIN_FID = new SelectList(db.Admins, "ADMIN_ID", "ADMIN_NAME", blog.ADMIN_FID);
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blogs.Find(id);
            db.Blogs.Remove(blog);
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
