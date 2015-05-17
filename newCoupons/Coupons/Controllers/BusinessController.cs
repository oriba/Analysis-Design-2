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
    public class BusinessController : BaseController
    {
        private CouponsContext db = new CouponsContext();

        // GET: Business
        public ActionResult Index(string sortOrder, string currentFilter, int? SelectedCategory, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CitySortParm = sortOrder == "City" ? "city_desc" : "City";

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

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(businesses.ToPagedList(pageNumber, pageSize));
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Business.Add(business);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.categoryID = new SelectList(db.Category, "ID", "ID", business.categoryID);
            ViewBag.ownerID = new SelectList(db.Owner, "ID", "ID", business.ownerID);
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
            ViewBag.categoryID = new SelectList(db.Category, "ID", "category", business.categoryID);
            ViewBag.ownerID = new SelectList(db.Owner, "ID", "ID", business.ownerID);
            return View(business);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var businessToUpdate = db.Business.Find(id);
            if (TryUpdateModel(businessToUpdate, "",
               new string[] { "ID","name","ownerID","categoryID","description","address","city","moneyEarned" }))
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
            return View(businessToUpdate);
        }

        // GET: Business/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
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
