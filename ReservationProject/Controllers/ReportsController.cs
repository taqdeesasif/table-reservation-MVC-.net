using ReservationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ReservationProject.Controllers
{
    [Authorize(Roles = "Manager,Data_Clerk")]
   
    public class ReportsController : Controller
    {
        // GET: Reports
        Model5 db = new Model5();

        public ActionResult ReportWithoutFood(FilterModel fm)
        {

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            { ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s"); }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Today.ToString("s");
                fm.DateTo = System.DateTime.Today;
            }
            else
            { ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s"); }

            ViewBag.TabCat = db.TabCategories.Select(x => new SelectListItem { Value = x.TABCATEGORY_ID.ToString(), Text = x.TABCATEGORY_NAME }).ToList();


            if (fm.TabCat == null)
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == false & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == false & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & x.TABCATEGORY_FID == fm.TabCat).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }





        }
        public ActionResult ReportWithFood(FilterModel fm)
        {
            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            { ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s"); }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Today.ToString("s");
                fm.DateTo = System.DateTime.Today;
            }
            else
            { ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s"); }



            ViewBag.ItemCat = db.ItemCategories.Select(x => new SelectListItem { Value = x.ITEMCATEGORY_ID.ToString(), Text = x.ITEMCATEGORY_NAME }).ToList();


            if (fm.ItemCat == null)
            {
                ViewBag.Itm = db.Items.Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList();
            }
            else
            { ViewBag.Itm = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList(); }

            var od = db.ReservationDetails.Select(x => x.RESERVATION_FID).ToList();

            if (fm.ItemCat != null)
            {
                var p = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => x.ITEM_ID).ToList();
                if (fm.Itm != null)
                {
                    p = db.Items.Where(x => x.ITEM_ID == fm.Itm).Select(x => x.ITEM_ID).ToList();
                }


                od = db.ReservationDetails.Where(x => p.Contains(x.ITEM_FID)).Select(x => x.RESERVATION_FID).ToList();

            }
            var o = db.Reservations.Where(x => x.WITH_PREORDER == true & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & od.Contains(x.RESERVATION_ID)).OrderByDescending(x => x.CHECKIN_DATE).ToList();

            return View(o);
        }

        public ActionResult SaleReport(FilterModel fm)
        {

            if (fm.DateFrom == null)
            {
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            }
            else
            { ViewBag.DateFrom = Convert.ToDateTime(fm.DateFrom).ToString("s"); }

            if (fm.DateTo == null)
            {
                ViewBag.DateTo = System.DateTime.Today.ToString("s");
                fm.DateTo = System.DateTime.Today;
            }
            else
            { ViewBag.DateTo = Convert.ToDateTime(fm.DateTo).ToString("s"); }

            ViewBag.TabCat = db.TabCategories.Select(x => new SelectListItem { Value = x.TABCATEGORY_ID.ToString(), Text = x.TABCATEGORY_NAME }).ToList();

            ViewBag.ItemCat = db.ItemCategories.Select(x => new SelectListItem { Value = x.ITEMCATEGORY_ID.ToString(), Text = x.ITEMCATEGORY_NAME }).ToList();


            if (fm.ItemCat == null)
            {
                ViewBag.Itm = db.Items.Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList();
            }
            else
            { ViewBag.Itm = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList(); }

            var od = db.ReservationDetails.Select(x => x.RESERVATION_FID).ToList();

            if (fm.ItemCat != null)
            {
                var p = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => x.ITEM_ID).ToList();
                if (fm.Itm != null)
                {
                    p = db.Items.Where(x => x.ITEM_ID == fm.Itm).Select(x => x.ITEM_ID).ToList();
                }


                od = db.ReservationDetails.Where(x => p.Contains(x.ITEM_FID)).Select(x => x.RESERVATION_FID).ToList();

            }


            if (fm.TabCat == null && (fm.ItemCat != null || fm.Itm != null))
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == true & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & od.Contains(x.RESERVATION_ID)).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else if (fm.TabCat != null && (fm.ItemCat != null || fm.Itm != null))
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == true & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & x.TABCATEGORY_FID == fm.TabCat & od.Contains(x.RESERVATION_ID)).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }

            else if (fm.TabCat == null && (fm.Itm == null || fm.ItemCat == null))
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else if (fm.TabCat != null && (fm.Itm == null || fm.ItemCat == null))
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & x.TABCATEGORY_FID == fm.TabCat).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }






        }

        public ActionResult DailyReport(FilterModel fm)
        {

           
                ViewBag.DateFrom = System.DateTime.Today.ToString("s");
                fm.DateFrom = System.DateTime.Today;
            
           

           
                ViewBag.DateTo = System.DateTime.Today.ToString("s");
                fm.DateTo = System.DateTime.Today;
            
           

            ViewBag.TabCat = db.TabCategories.Select(x => new SelectListItem { Value = x.TABCATEGORY_ID.ToString(), Text = x.TABCATEGORY_NAME }).ToList();

            ViewBag.ItemCat = db.ItemCategories.Select(x => new SelectListItem { Value = x.ITEMCATEGORY_ID.ToString(), Text = x.ITEMCATEGORY_NAME }).ToList();


            if (fm.ItemCat == null)
            {
                ViewBag.Itm = db.Items.Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList();
            }
            else
            { ViewBag.Itm = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList(); }

            var od = db.ReservationDetails.Select(x => x.RESERVATION_FID).ToList();

            if (fm.ItemCat != null)
            {
                var p = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => x.ITEM_ID).ToList();
                if (fm.Itm != null)
                {
                    p = db.Items.Where(x => x.ITEM_ID == fm.Itm).Select(x => x.ITEM_ID).ToList();
                }


                od = db.ReservationDetails.Where(x => p.Contains(x.ITEM_FID)).Select(x => x.RESERVATION_FID).ToList();

            }


            if (fm.TabCat == null && (fm.ItemCat != null || fm.Itm != null))
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == true & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & od.Contains(x.RESERVATION_ID)).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else if (fm.TabCat != null && (fm.ItemCat != null || fm.Itm != null))
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == true & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & x.TABCATEGORY_FID == fm.TabCat & od.Contains(x.RESERVATION_ID)).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }

            else if (fm.TabCat == null && (fm.Itm == null || fm.ItemCat == null))
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else if (fm.TabCat != null && (fm.Itm == null || fm.ItemCat == null))
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & x.TABCATEGORY_FID == fm.TabCat).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }






        }


        public ActionResult WeeklyReport(FilterModel fm)
        {


            ViewBag.DateFrom = System.DateTime.Today.ToString("s");
            fm.DateFrom = System.DateTime.Today;




            ViewBag.DateTo = System.DateTime.Today.AddDays(7).ToString("s");
            fm.DateTo = System.DateTime.Today.AddDays(7);



            ViewBag.TabCat = db.TabCategories.Select(x => new SelectListItem { Value = x.TABCATEGORY_ID.ToString(), Text = x.TABCATEGORY_NAME }).ToList();

            ViewBag.ItemCat = db.ItemCategories.Select(x => new SelectListItem { Value = x.ITEMCATEGORY_ID.ToString(), Text = x.ITEMCATEGORY_NAME }).ToList();


            if (fm.ItemCat == null)
            {
                ViewBag.Itm = db.Items.Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList();
            }
            else
            { ViewBag.Itm = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => new SelectListItem { Value = x.ITEM_ID.ToString(), Text = x.ITEM_NAME + " (" + x.ITEM_DESCRIPTION + ")" }).ToList(); }

            var od = db.ReservationDetails.Select(x => x.RESERVATION_FID).ToList();

            if (fm.ItemCat != null)
            {
                var p = db.Items.Where(x => x.ITEMCATEGORY_FID == fm.ItemCat).Select(x => x.ITEM_ID).ToList();
                if (fm.Itm != null)
                {
                    p = db.Items.Where(x => x.ITEM_ID == fm.Itm).Select(x => x.ITEM_ID).ToList();
                }


                od = db.ReservationDetails.Where(x => p.Contains(x.ITEM_FID)).Select(x => x.RESERVATION_FID).ToList();

            }


            if (fm.TabCat == null && (fm.ItemCat != null || fm.Itm != null))
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == true & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & od.Contains(x.RESERVATION_ID)).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else if (fm.TabCat != null && (fm.ItemCat != null || fm.Itm != null))
            {
                var o = db.Reservations.Where(x => x.WITH_PREORDER == true & x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & x.TABCATEGORY_FID == fm.TabCat & od.Contains(x.RESERVATION_ID)).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }

            else if (fm.TabCat == null && (fm.Itm == null || fm.ItemCat == null))
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else if (fm.TabCat != null && (fm.Itm == null || fm.ItemCat == null))
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo & x.TABCATEGORY_FID == fm.TabCat).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }
            else
            {
                var o = db.Reservations.Where(x => x.CHECKIN_DATE >= fm.DateFrom & x.CHECKIN_DATE <= fm.DateTo).OrderByDescending(x => x.CHECKIN_DATE).ToList();
                return View(o);
            }






        }

        public ActionResult Invoice(int id)
        {
            var o = db.Reservations.Where(x => x.RESERVATION_ID == id).ToList();
            return View(o);
        }




    }
}