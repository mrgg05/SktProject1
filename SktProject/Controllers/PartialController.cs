using SktProject.Models;
using SktProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SktProject.Controllers
{
    public class PartialController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Partial
        public ActionResult Index()
        {
            return View();
        }


      
        public ActionResult CatergoryPartial()
        {
            var result = (from c in db.Categories
                          select new CategoriesViewModels
                          {
                              CategoryName=c.CategoryName
                          }).ToList();



            return PartialView(result);

        }
    }
}