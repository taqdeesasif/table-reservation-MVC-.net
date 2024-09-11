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
    public class ItemsController : Controller
    {
        private Model5 db = new Model5();

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.ItemCategory);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.ITEMCATEGORY_FID = new SelectList(db.ItemCategories, "ITEMCATEGORY_ID", "ITEMCATEGORY_NAME");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                item.ITEM_PIC.SaveAs(Server.MapPath("~/ItemPicture/" + item.ITEM_PIC.FileName));
                item.ITEM_PICTURE = "~/ItemPicture/" + item.ITEM_PIC.FileName;


                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ITEMCATEGORY_FID = new SelectList(db.ItemCategories, "ITEMCATEGORY_ID", "ITEMCATEGORY_NAME", item.ITEMCATEGORY_FID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
           
            Item item = db.Items.Find(id);
           
            ViewBag.ITEMCATEGORY_FID = new SelectList(db.ItemCategories, "ITEMCATEGORY_ID", "ITEMCATEGORY_NAME", item.ITEMCATEGORY_FID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ITEM_PIC != null)
                {

                    item.ITEM_PIC.SaveAs(Server.MapPath("~/ItemPicture/" + item.ITEM_PIC.FileName));
                    item.ITEM_PICTURE = "~/ItemPicture/" + item.ITEM_PIC.FileName;

                }

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ITEMCATEGORY_FID = new SelectList(db.ItemCategories, "ITEMCATEGORY_ID", "ITEMCATEGORY_NAME", item.ITEMCATEGORY_FID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
