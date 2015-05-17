using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Coupons.DAL;
using Coupons.Models;
using PagedList;

namespace Coupons.Controllers
{
    public class CouponMakerController : BaseController
    {
        private CouponsContext db = new CouponsContext();

        // GET: CouponMaker
        public ActionResult Index(string sortOrder, string currentFilter, int? page, int? SelectedCategory, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var categories = db.Category.OrderBy(q => q.ID).ToList();
            ViewBag.SelectedCategory = new SelectList(categories, "ID", "category", SelectedCategory);
            int categoryID = SelectedCategory.GetValueOrDefault();

            IQueryable<CouponMaker> couponMakers = db.CouponMaker
                .Where(c => !SelectedCategory.HasValue || c.Business.categoryID == categoryID)
                .OrderBy(d => d.Name);
            var sql = couponMakers.ToString();
            if (!String.IsNullOrEmpty(searchString))
            {
                couponMakers = couponMakers.Where(s => s.Business.city.Contains(searchString)
                                       || s.Business.address.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    couponMakers = couponMakers.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    couponMakers = couponMakers.OrderBy(s => s.startDate);
                    break;
                case "date_desc":
                    couponMakers = couponMakers.OrderByDescending(s => s.startDate);
                    break;
                case "Price":
                    couponMakers = couponMakers.OrderBy(s => s.couponPrice);
                    break;
                case "price_desc":
                    couponMakers = couponMakers.OrderByDescending(s => s.couponPrice);
                    break;
                default:
                    couponMakers = couponMakers.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(couponMakers.ToPagedList(pageNumber, pageSize));
        }

        // GET: CouponMaker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponMaker couponMaker = db.CouponMaker.Find(id);
            if (couponMaker == null)
            {
                return HttpNotFound();
            }
            return View(couponMaker);
        }

        // GET: CouponMaker/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.Status, "status", "status");
            ViewBag.BusinessID = new SelectList(db.Business, "ID", "name");

            return View();
        }

        // POST: CouponMaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description,originalPrice,couponPrice,rating,numOfRaters,startDate,endDate,quantity,maxQuantity,StatusID,BusinessID")] CouponMaker couponMaker)
        {
            try {
                if (ModelState.IsValid)
                {
                    db.CouponMaker.Add(couponMaker);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.StatusID = new SelectList(db.Status, "status", "status");
            ViewBag.BusinessID = new SelectList(db.Business, "ID", "name");
            return View(couponMaker);
        }

        // GET: CouponMaker/Create
        public ActionResult Order()
        {
            ViewBag.CouponMakerID = new SelectList(db.CouponMaker, "ID", "name");
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "UserName");
            return View();
        }

        // POST: CouponMaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order([Bind(Include = "ID,isActive,CouponMakerID,CustomerID")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Coupon.Add(coupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponMakerID = new SelectList(db.CouponMaker, "ID", "name", coupon.CouponMakerID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "UserName", coupon.CustomerID);
            return View(coupon);
        }

        // GET: CouponMaker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponMaker couponMaker = db.CouponMaker.Find(id);
            if (couponMaker == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.Status, "ID", "status", couponMaker.StatusID);
            return View(couponMaker);
        }

        // POST: CouponMaker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var couponMakerToUpdate = db.CouponMaker.Find(id);
            if (TryUpdateModel(couponMakerToUpdate, "",
               new string[] { "ID,Name,description,originalPrice,couponPrice,rating,numOfRaters,startDate,endDate,quantity,maxQuantity,status" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(couponMakerToUpdate);
        }

        // GET: CouponMaker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CouponMaker couponMaker = db.CouponMaker.Find(id);
            if (couponMaker == null)
            {
                return HttpNotFound();
            }
            return View(couponMaker);
        }

        // POST: CouponMaker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CouponMaker couponMaker = db.CouponMaker.Find(id);
            db.CouponMaker.Remove(couponMaker);
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
