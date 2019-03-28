using SktProject.Models;
using SktProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SktProject.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
      
        // GET: Checkout
       

        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);
          
            try
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);
                order.UserName = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                order.Total = cart.GetTotal();

                string[] args = { "E-Ticaret", "İyi günlerde kullanın"};
                Main(args);
                //Save Order
                storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                   
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.UserName == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

     


        public void Main(string[] args)
        {
            string email = User.Identity.Name;
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.Credentials = new NetworkCredential("gurkangulinfo@gmail.com", "Gg.12345");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("gurkangulinfo@gmail.com", "GulSoft");
            mail.To.Add(email);
            mail.Subject = args[0];
            mail.IsBodyHtml = true;
            mail.Body = args[1];
            sc.Send(mail);
        }

    }
}