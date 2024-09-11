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
    public class TabCategoriesController : Controller
    {
        private Model5 db = new Model5();

        // GET: TabCategories
        public ActionResult Index()
        {
            return View(db.TabCategories.ToList());
        }

        // GET: TabCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TabCategory tabCategory = db.TabCategories.Find(id);
            if (tabCategory == null)
            {
                return HttpNotFound();
            }
            return View(tabCategory);
        }

        // GET: TabCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TabCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TabCategory tabCategory)
        {
            if (ModelState.IsValid)
            {

                tabCategory.TAB_PIC.SaveAs(Server.MapPath("~/TabPicture/" + tabCategory.TAB_PIC.FileName));
                tabCategory.TABCATEGORY_PICTURE = "~/TabPicture/" + tabCategory.TAB_PIC.FileName;




                db.TabCategories.Add(tabCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tabCategory);
        }

        // GET: TabCategories/Edit/5
        public ActionResult Edit(int? id)
        {
           
            TabCategory tabCategory = db.TabCategories.Find(id);
           
            return View(tabCategory);
        }

        // POST: TabCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TabCategory tabCategory)
        {
            if (ModelState.IsValid)
            {
                if (tabCategory.TAB_PIC != null)
                {

                    tabCategory.TAB_PIC.SaveAs(Server.MapPath("~/TabPicture/" + tabCategory.TAB_PIC.FileName));
                    tabCategory.TABCATEGORY_PICTURE = "~/TabPicture/" + tabCategory.TAB_PIC.FileName;



                }

                db.Entry(tabCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tabCategory);
        }

        // GET: TabCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TabCategory tabCategory = db.TabCategories.Find(id);
            if (tabCategory == null)
            {
                return HttpNotFound();
            }
            return View(tabCategory);
        }

        // POST: TabCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TabCategory tabCategory = db.TabCategories.Find(id);
            db.TabCategories.Remove(tabCategory);
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
