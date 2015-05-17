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
    public class CouponController : BaseController
    {
        private CouponsContext db = new CouponsContext();

        // GET: Coupon
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, string nameSearch)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.ActiveSortParm = String.IsNullOrEmpty(sortOrder) ? "active_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;           
            var coupon = from s in db.Coupon
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                coupon = coupon.Where(s => s.ID.ToString().Contains(searchString));
            }

            if (!String.IsNullOrEmpty(nameSearch))////////////////////////////////////
            {
                coupon = coupon.Where(s => s.Customer.UserName.Contains(nameSearch));
            }///////////////////////////////////////////////////////
            switch (sortOrder)
            {
                case "active_desc":
                    coupon = coupon.OrderByDescending(s => s.isActive);
                    break;
                default:
                    coupon = coupon.OrderBy(s => s.isActive);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(coupon.ToPagedList(pageNumber, pageSize));
        }

        // GET: Coupon/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupon.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // GET: Coupon/Create
        public ActionResult Create()
        {
            //ViewBag.Coupon = new SelectList(db.Coupon, "ID", "ID");
            ViewBag.CouponMakerID = new SelectList(db.CouponMaker, "ID", "name");
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "firstName");
            //ViewBag.CustomerID = new SelectList(db.Customer, "ID", "lastName");
            //ViewBag.CustomerID = new SelectList(db.Customer, "ID", "ID");
            //ViewBag.CouponMakerID = new SelectList(db.Customer, "ID", "endDate");
            //ViewBag.CouponMakerID = new SelectList(db.Customer, "ID", "couponPrice");
            //ViewBag.CouponMakerID = new SelectList(db.Customer, "ID", "isActive");
            return View();
        }

        // POST: Coupon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,isActive,CouponMakerID,CustomerID")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Coupon.Add(coupon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CouponMakerID = new SelectList(db.CouponMaker, "ID", "name", coupon.CouponMakerID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "firstName", coupon.CustomerID);
            return View(coupon);
        }

        // GET: Coupon/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupon.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            ViewBag.CouponMakerID = new SelectList(db.CouponMaker, "ID", "name", coupon.CouponMakerID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "firstName", coupon.CustomerID);
            return View(coupon);
        }

        // POST: Coupon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var couponsToUpdate = db.Coupon.Find(id);
            if (TryUpdateModel(couponsToUpdate, "",
               new string[] { "isActive" }))
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
            return View(couponsToUpdate);
        }

        // GET: Coupon/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coupon coupon = db.Coupon.Find(id);
            if (coupon == null)
            {
                return HttpNotFound();
            }
            return View(coupon);
        }

        // POST: Coupon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coupon coupon = db.Coupon.Find(id);
            db.Coupon.Remove(coupon);
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
