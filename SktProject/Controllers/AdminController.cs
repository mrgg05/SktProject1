using Microsoft.AspNet.Identity;
using SktProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SktProject.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Products()
        {
            var userID = User.Identity.GetUserId();
            return View(db.Products.Where(x=>x.User.Id==userID).ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public JsonResult Delete(string  id)
        {
            int no = Convert.ToInt32(id);
            var productlist = db.Products.Where(x => x.ProductId == no).FirstOrDefault();
            db.Products.Remove(productlist);
            db.SaveChanges();

          

            return Json(productlist);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductAdd(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(image.InputStream);
                FileInfo fotoinfo = new FileInfo(image.FileName);
                string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                img.Resize(500, 775);
                img.Save("../Uploads/Photo/" + newfoto);
                product.ProductUrl = "../Uploads/Photo/" + newfoto;
                
                string id = User.Identity.GetUserId();

                var userid = db.Users.Where(x => x.Id == id).FirstOrDefault();
                product.User = userid;
                
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Products", "Admin");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return RedirectToAction("Products", "Admin");

        }


    }
}