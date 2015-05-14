using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Coupons.DAL;
using Coupons.ViewModels;

namespace Coupons.Controllers
{
    public class HomeController : BaseController
    {
        private CouponsContext db = new CouponsContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            //return View();
            IQueryable<PurchaseDateGroup> data = from customer in db.Customer
                                                 group customer by customer.age into ageGroup
                                                 select new PurchaseDateGroup()
                                                 {
                                                     Age = ageGroup.Key,
                                                     CustomerCount = ageGroup.Count()
                                                 };
            return View(data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}