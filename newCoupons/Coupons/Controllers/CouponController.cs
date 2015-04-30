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

namespace Coupons.Controllers
{
    public class CouponController : Controller
    {
        private CouponsContext db = new CouponsContext();

        // GET: Coupon
        public ActionResult Index()
        {
            var coupon = db.Coupon.Include(c => c.CouponMaker).Include(c => c.Customer);
            return View(coupon.ToList());
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
            ViewBag.CouponMakerID = new SelectList(db.CouponMaker, "ID", "name");
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "firstName");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,isActive,CouponMakerID,CustomerID")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coupon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CouponMakerID = new SelectList(db.CouponMaker, "ID", "name", coupon.CouponMakerID);
            ViewBag.CustomerID = new SelectList(db.Customer, "ID", "firstName", coupon.CustomerID);
            return View(coupon);
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
