using ReservationProject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net;
using System.Net.Mail;

namespace ReservationProject.Controllers
{
    public class HomeController : Controller
    {
        Model5 db = new Model5();
        public ActionResult IndexCustomer()
        {
            return View();
        }
        [Authorize]
        public ActionResult IndexAdmin()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin a)
        {
            Admin ad = db.Admins.Where(x => x.ADMIN_EMAIL == a.ADMIN_EMAIL && x.ADMIN_PASSWORD == a.ADMIN_PASSWORD).FirstOrDefault();
            if (ad != null)
            {
                FormsAuthentication.SetAuthCookie(ad.ADMIN_NAME, false);

                Session["Admin"] = ad;
                return RedirectToAction("IndexAdmin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();

            }


        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Admin"] = null;

            return RedirectToAction("Login");
        }


        public ActionResult Feedback()
        {
            return View();
        }
        public ActionResult Reservation()
        {


            return View();
        }
        public ActionResult DisplayMenu(int? id)
        {
            DisplayMenuModel s = new DisplayMenuModel();

            s.Cat = db.ItemCategories.ToList();
            if (id == null)
            {
                s.Catt = db.ItemCategories.ToList();
                s.Pro = db.Items.ToList();
            }
            else
            {
                s.Catt = db.ItemCategories.Where(p => p.ITEMCATEGORY_ID == id).ToList();
                s.Pro = db.Items.Where(p => p.ITEMCATEGORY_FID == id).ToList();
            }


            return View(s);
        }



        public ActionResult AddMenu(int? id)
        {
            AddMenuModel s = new AddMenuModel();
            s.Ca = db.ItemCategories.ToList();
            if (id == null)
            {
                s.Caa = db.ItemCategories.ToList();
                s.Pr = db.Items.ToList();
            }
            else
            {
                s.Caa = db.ItemCategories.Where(p => p.ITEMCATEGORY_ID == id).ToList();
                s.Pr = db.Items.Where(p => p.ITEMCATEGORY_FID == id).ToList();
            }


            return View(s);
        }








        public ActionResult DisplayTables()
        {
            AvailabilityModel a = new AvailabilityModel();
            a.Tb = db.TabCategories.ToList();
            a.Ab = db.Availibilities.ToList();
            return View(a);
        }


        public ActionResult DisplayStaff()
        {
            return View(db.Staffs.Where(p => p.StaffJob.STAFFJOB_NAME == "Chef").ToList());
        }
        public ActionResult Blog()
        {
            return View(db.Blogs.ToList());
        }

        public ActionResult BlogSingle(int id)
        {


            return View(db.Blogs.Where(p => p.BLOG_ID == id).ToList());
        }

        public ActionResult AddToCart(int id)
        {
            List<Item> list;
            if (Session["myCart"] == null)
            { list = new List<Item>(); }
            else
            {
                list = (List<Item>)Session["myCart"];
            }
            Boolean isItemExist = false;
            foreach(var item in list)
            {
                if(id == item.ITEM_ID)
                {
                    isItemExist = true;
                    item.IT_QUANTITY++;
                }
            }
            if(isItemExist == false)
            {
                list.Add(db.Items.Where(p => p.ITEM_ID == id).FirstOrDefault());
                list[list.Count - 1].IT_QUANTITY = 1;
            }
            
           
            Session["myCart"] = list;
            return RedirectToAction("Reservation");

        }

        public ActionResult MinusFromCart(int RowNo)
        {
            List<Item> list = (List<Item>)Session["myCart"];

            list[RowNo].IT_QUANTITY--;
            if (list[RowNo].IT_QUANTITY == 0)
                list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("Reservation");

        }
        public ActionResult PlusToCart(int RowNo)
        {
            List<Item> list = (List<Item>)Session["myCart"];
            if (list[RowNo].IT_QUANTITY >= 10)
            {
                return RedirectToAction("Reservation");
            }
            else
            {
                list[RowNo].IT_QUANTITY++;
                Session["myCart"] = list;
                return RedirectToAction("Reservation");
            }

        }
        public ActionResult RemoveFromCart(int RowNo)
        {
            List<Item> list = (List<Item>)Session["myCart"];

            list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("Reservation");

        }

        public ActionResult AddTable(int id)
        {
           


            List<TabCategory> dt = new List<TabCategory>();
            dt.Add(db.TabCategories.Where(p => p.TABCATEGORY_ID == id).FirstOrDefault());
            Session["myTable"] = dt;
            return RedirectToAction("Reservation");
        }

        public ActionResult Comment(Feedback f )
        {
            f.FEDDBACK_DATE = DateTime.Now;
            Customer c = (Customer)Session["Customer"];
            f.CUSTOMER_FIDD = c.CUSTOMER_ID;

            Session["Feedback"] = f;

           

            db.Feedbacks.Add(f);
            db.SaveChanges();

            return  RedirectToAction("Feedback");
        }

        public ActionResult Cont(ContactU u)
        {
           u.CONTACTUS_DATE = DateTime.Now;
            Customer c = (Customer)Session["Customer"];
           u.CUSTOMER_FIDDD = c.CUSTOMER_ID;

            Session["Contactus"] = u;
            //1 Mail

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("foodoclockr@gmail.com");
            mail.To.Add(u.CONTACTUS_EMAIL);
            mail.Subject = "Contact Food O'clock Resturant";
            mail.Body = "<b>Food O'clock!</b> <br/>Hello!!! Tell us more about your problem...So that, we can further asist you in this matter.....";
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("foodoclockr@gmail.com", "Pakistan_123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            

            db.ContactUs.Add(u);
            db.SaveChanges();

            return RedirectToAction("Contact");
        }


        public ActionResult PayNow(Reservation r)
        {
            if (r.CHECKIN_DATE == DateTime.Today && r.CHECKIN_TIME < DateTime.Now.TimeOfDay)
            {
                TempData["Message"] = "Please Enter Future Date Time Slots!!!";
                return RedirectToAction("InvalidReservation");

            }

            int i = db.Reservations.Where(x => x.TABCATEGORY_FID == r.TABCATEGORY_FID & x.CHECKIN_DATE == r.CHECKIN_DATE & x.CHECKIN_TIME == r.CHECKIN_TIME).Count();
            if(i>=10)
            {
                TempData["Message"] = "This Table is not available!!!";
                return RedirectToAction("InvalidReservation");
            }

            int o = db.Availibilities.Where(x => x.UNAVAILABLE_DATE == r.CHECKIN_DATE & (x.UNAVAILABLE_TIME == r.CHECKIN_TIME || x.UNAVAILABLE_TIME == null)).Count();
            if (o == 1)
            {
                TempData["Message"] = "This Date Time slot is not available";
                return RedirectToAction("InvalidReservation");
            }



            else
            {
                r.DATE_TIME = DateTime.Now;
                r.STATUS = "Active";
                r.ORDER_STATUS = "Booked";
           
                if (Session["myCart"] != null)
                { r.WITH_PREORDER = true; }
                else
                { r.WITH_PREORDER = false; }

                if(Session["Customer"] != null)
                {
                    Customer c = (Customer)Session["Customer"];
                    r.CUSTOMER_FID = c.CUSTOMER_ID;
                }

                Session["Reservation"] = r;
                if(r.RESERVATION_STATUS == "Cash On Service")
                {
                    return RedirectToAction("Booking");
                }
                else
                {
                    return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=foodoclockr@gmail.com&item_name=FoodOclockResturant&return=https://localhost:44353/Home/Booking&amount=" + double.Parse(r.TOTAL_BILL.ToString()) / 160);

                }


            }



        }

        public ActionResult Booking()
        {
            Reservation r = (Reservation)Session["Reservation"];

           

            // 1. Code for sending Email
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("foodoclockr@gmail.com");
            mail.To.Add(r.CUSTOMER_EMAIL);
            mail.Subject = "Reservation Confirmation";
            mail.Body = "<b>Food O'clock!</b> <br/> Dear "+r.CUSTOMER_NAME+"<br/>Your Table is Booked on "+r.CHECKIN_DATE.ToLongDateString() + +r.CHECKIN_TIME+".<br/>Your Total Bill is: "+r.TOTAL_BILL+"<br/>Thanks For Your Trust.....";
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("foodoclockr@gmail.com",                                                              "Pakistan_123");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            //2. SMS 
            String api = "https://lifetimesms.com/json?api_token=3ffe8aa5e2549570ad5a43c2c69e6b48b2a4815471&api_secret=FoodOclock&to=" + r.CUSTOMER_CONTACT + "&from=FoodOclock&message=Food Oclock!!!!Table Booking Confirmed....Thanks for your trust";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);

                var httpResponse = (HttpWebResponse)req.GetResponse();

    
                
            //3.save in database

            db.Reservations.Add(r);
            db.SaveChanges();

            if (Session["myCart"] != null)
            {
                List<Item> t = (List<Item>)Session["myCart"];
                for (int i = 0; i < t.Count; i++)
                {
                    ReservationDetail rd = new ReservationDetail();
                    int reservationID = db.Reservations.Max(x => x.RESERVATION_ID);
                    rd.RESERVATION_FID = reservationID;
                    rd.ITEM_FID = t[i].ITEM_ID;
                    rd.QUANTITY = t[i].IT_QUANTITY;
                    rd.SALE_PRICE = t[i].ITEM_SALEPRICE;
                    db.ReservationDetails.Add(rd);
                    db.SaveChanges();


                }


            }


            return RedirectToAction("OrderBooked");



        }


        public ActionResult OrderBooked()
        {
            return View();
        }

        public ActionResult CloseOrder()
        {
            Session["myCart"] = null;
            Session["Reservation"] = null;
            return RedirectToAction("IndexCustomer");
        }

        public ActionResult InvalidReservation()
        {

            return View(db.Availibilities.ToList());

        }





      


       

    }
}