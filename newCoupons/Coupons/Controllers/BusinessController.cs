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
    public class BusinessController : Controller
    {
        private CouponsContext db = new CouponsContext();

        // GET: Business
        public ActionResult Index(string sortOrder, int? SelectedCategory, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CitySortParm = sortOrder == "City" ? "city_desc" : "City";

            var categories = db.Category.OrderBy(q => q.ID).ToList();
            ViewBag.SelectedCategory = new SelectList(categories, "ID", "category", SelectedCategory);
            int categoryID = SelectedCategory.GetValueOrDefault();

            IQueryable<Business> businesses = db.Business
                .Where(c => !SelectedCategory.HasValue || c.categoryID == categoryID)
                .OrderBy(d => d.name);
            var sql = businesses.ToString();
            if (!String.IsNullOrEmpty(searchString))
            {
                businesses = businesses.Where(s => s.city.Contains(searchString)
                                       || s.address.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    businesses = businesses.OrderByDescending(s => s.name);
                    break;
                case "City":
                    businesses = businesses.OrderBy(s => s.city);
                    break;
                case "city_desc":
                    businesses = businesses.OrderByDescending(s => s.city);
                    break;
                default:
                    businesses = businesses.OrderBy(s => s.name);
                    break;
            }

            return View(businesses.ToList());
        }

        // GET: Business/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Business.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // GET: Business/Create
        public ActionResult Create()
        {
            ViewBag.categoryID = new SelectList(db.Category, "ID", "ID");
            ViewBag.ownerID = new SelectList(db.Owner, "ID", "firstName");
            return View();
        }

        // POST: Business/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,name,ownerID,categoryID,description,address,city,moneyEarned")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Business.Add(business);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categoryID = new SelectList(db.Category, "ID", "ID", business.categoryID);
            ViewBag.ownerID = new SelectList(db.Owner, "ID", "firstName", business.ownerID);
            return View(business);
        }

        // GET: Business/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Business.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryID = new SelectList(db.Category, "ID", "ID", business.categoryID);
            ViewBag.ownerID = new SelectList(db.Owner, "ID", "firstName", business.ownerID);
            return View(business);
        }

        // POST: Business/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,name,ownerID,categoryID,description,address,city,moneyEarned")] Business business)
        {
            if (ModelState.IsValid)
            {
                db.Entry(business).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryID = new SelectList(db.Category, "ID", "ID", business.categoryID);
            ViewBag.ownerID = new SelectList(db.Owner, "ID", "firstName", business.ownerID);
            return View(business);
        }

        // GET: Business/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Business business = db.Business.Find(id);
            if (business == null)
            {
                return HttpNotFound();
            }
            return View(business);
        }

        // POST: Business/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Business business = db.Business.Find(id);
            db.Business.Remove(business);
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
