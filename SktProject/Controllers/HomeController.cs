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
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Login()
        {
            //return RedirectToAction("Index1", "Categories");

            

            return View();
        }


        public ActionResult checkout()
        {
            //return RedirectToAction("Index1", "Categories");
            var cart = ShoppingCart.GetCart(this.HttpContext);


            var son = new CategoryProductViewModels
            {
                Cart = cart.GetCartItems(),
                Product = db.Products.ToList(),
                Category = db.Categories.ToList()

            };

            return View(son);

        }

        public ActionResult Partiala()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult checkoutPayment(CategoryProductViewModels values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);
                order = values.Order;
                order.UserName = User.Identity.Name;
                order.OrderDate = DateTime.Now;
                
                order.Total = cart.GetTotal();

                //string[] args = { "E-Ticaret", "İyi günlerde kullanın" };
                //Main(args);
                //Save Order
                db.Orders.Add(order);
                db.SaveChanges();
                //Process the order

                cart.CreateOrder(order);

                return RedirectToAction("Complete","Home",
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
            bool isValid = db.Orders.Any(
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

        public ActionResult Index()
        {
            //return RedirectToAction("Index1", "Categories");

            var son = new CategoryProductViewModels
            {
                Product = db.Products.ToList(),
                Category = db.Categories.ToList()

            };

            return View(son);
        }

        public ActionResult Index2()
        {
            //return RedirectToAction("Index1", "Categories");
            //var result = (from p in db.Products
                          
            //              select new ProductsViewModels
            //              {
                            
            //                 Price= p.Price,
            //                  ProductId=p.ProductId,
            //                 ProductionDate= p.ProductionDate,
            //                  ProductUrl=p.ProductUrl,
            //                  SKT=p.SKT,
            //                  TETT=p.TETT,
            //                  Title=p.Title,
            //                  User=p.User
            //              }).ToList();

            var son = new CategoryProductViewModels
            {
                Product = db.Products.ToList(),
                Category=db.Categories.ToList()
                
            };

            return View(son);
        }

        public ActionResult IndexId(int id)
        {
            //int id1 = Convert.ToInt32(id);
            //return RedirectToAction("Index1", "Categories");
            var result = (from p in db.Products
                          where p.CategoryId == id
                          select new CategoryProductViewModels
                          {
                              Product=db.Products.ToList(),
                              Category=db.Categories.ToList()
                              //CategoryId = c.CategoryName,
                              //Price = p.Price,
                              //ProductId = p.ProductId,
                              //ProductionDate = p.ProductionDate,
                              //ProductUrl = p.ProductUrl,
                              //SKT = p.SKT,
                              //TETT = p.TETT,
                              //Title = p.Title,
                              //User = p.User
                          });

            CategoryProductViewModels son = new CategoryProductViewModels
            {
                Product = db.Products.Where(x => x.CategoryId == id).ToList(),
                Category = db.Categories.ToList()
            };

            return View(son);
        }




    


        [HttpGet]
        public JsonResult IndexProduct()
        {
            var result = (from p in db.Products
                         select new IndexViewModels
                         {
                             ProductId=p.ProductId,
                             SKT = p.SKT,
                             Price = p.Price,
                             ProductName = p.Title,
                             ProductUrl = p.ProductUrl
                         }).ToList();
                       


            return Json(result,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IndexCat()
        {
            var result = (from p in db.Categories
                          select new CategoriesViewModels
                          {
                              CategoryId = p.CategoryId,
                              CategoryName = p.CategoryName
                          }).ToList();
                         


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetCatProduct(Category cat)
        {
            var id = Convert.ToInt32(cat.CategoryId);

            var catproduct = (from p in db.Products
                              join c in db.Categories on p.CategoryId equals c.CategoryId
                              where p.CategoryId==id
                              select new
                              {
                                  p.ProductId,
                                  p.Price,
                                  p.SKT,
                                  p.TETT,
                                  p.Title,
                                  p.ProductUrl,
                                  p.ProductionDate,
                                  p.CategoryId
                              }).ToList();


            return Json(catproduct,JsonRequestBehavior.AllowGet);
        }





      
    }
}