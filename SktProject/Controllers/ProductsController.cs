using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SktProject.Models;
using System.IO;
using System.Web.Helpers;
using SktProject.Models.ViewModel;
using Microsoft.AspNet.Identity;

namespace SktProject.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index1()
        {
            return View();
        }
        // GET: Products
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public JsonResult Index()
        {
            var userID = User.Identity.GetUserId();
            var products = (from p in db.Products
                           join c in db.Categories on p.CategoryId equals c.CategoryId
                            where p.User.Id==userID
                           select new ProductsViewModels {

                               CategoryId=c.CategoryName,
                               Price=p.Price,
                               SKT=p.SKT,
                               ProductId=p.ProductId,
                               ProductionDate=p.ProductionDate,
                               ProductUrl=p.ProductUrl,
                               TETT=p.TETT,
                               Title=p.Title
                           }).ToList();
           

            return Json(products,JsonRequestBehavior.AllowGet);
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                WebImage img = new WebImage(image.InputStream);
                FileInfo fotoinfo = new FileInfo(image.FileName);
                string newfoto = Guid.NewGuid().ToString()+ fotoinfo.Extension;
                img.Resize(500, 775);
                img.Save("../Uploads/Photo/" + newfoto);
                product.ProductUrl = "../Uploads/Photo/" + newfoto;
                string id = User.Identity.GetUserId();

                var userid = db.Users.Where(x => x.Id == id).FirstOrDefault();
                product.User = userid;
                db.Products.Add(product);
                db.SaveChanges();
               
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return RedirectToAction("Products", "Admin");
        }



        // GET: Products/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CategoryId,Title,Price,ProductUrl,SKT,TETT,ProductionDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public JsonResult Delete(Product product)
        {
            
            var products = db.Products.Remove(product);
            
            return Json(products);
        }


        [Authorize(Roles = "Admin")]
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
