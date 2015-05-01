﻿using System;
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
    public class CouponMakerController : Controller
    {
        private CouponsContext db = new CouponsContext();

        // GET: CouponMaker
        public ActionResult Index(string sortOrder, int? page, int? SelectedCategory, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

        //    var couponMakers = from s in db.CouponMaker
        //                       select s;
            var categories = db.Category.OrderBy(q => q.ID).ToList();
            ViewBag.SelectedCategory = new SelectList(categories, "ID", "category", SelectedCategory);
            int categoryID = SelectedCategory.GetValueOrDefault();

            IQueryable<CouponMaker> couponMakers = db.CouponMaker
                .Where(c => !SelectedCategory.HasValue || c.Business.categoryID == categoryID)
                .OrderBy(d => d.name);
            var sql = couponMakers.ToString();
            if (!String.IsNullOrEmpty(searchString))
            {
                couponMakers = couponMakers.Where(s => s.Business.city.Contains(searchString)
                                       || s.Business.address.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    couponMakers = couponMakers.OrderByDescending(s => s.name);
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
                    couponMakers = couponMakers.OrderBy(s => s.name);
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
            return View();
        }

        // POST: CouponMaker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,description,originalPrice,couponPrice,rating,numOfRaters,startDate,endDate,quantity,maxQuantity,status")] CouponMaker couponMaker)
        {
            if (ModelState.IsValid)
            {
                db.CouponMaker.Add(couponMaker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(couponMaker);
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
            return View(couponMaker);
        }

        // POST: CouponMaker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,description,originalPrice,couponPrice,rating,numOfRaters,startDate,endDate,quantity,maxQuantity,status")] CouponMaker couponMaker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(couponMaker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(couponMaker);
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
