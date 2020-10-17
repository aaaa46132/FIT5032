using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032Assignment.Models;
using PagedList;

namespace FIT5032Assignment.Controllers
{
    public class StaffController : Controller
    {
        private NextGenDatabaseEntities db = new NextGenDatabaseEntities();

        // GET: Staff
        public ActionResult Index()
        {
            return View(db.Staffs.ToList());
        }
        public ActionResult Display(string option, string search, int? pageNumber, string sort)
        {
            ViewBag.SortByFirstName = string.IsNullOrEmpty(sort) ? "descending first name" : "first name";
            ViewBag.SortByLastName = string.IsNullOrEmpty(sort) ? "descending last name" : "";
            var records = db.Staffs.AsQueryable();
            if (option == "First Name")
            {
                records = records.Where(x => x.FirstName.StartsWith(search) || search == null);
            }
            else if (option == "Last Name")
            {
                records = records.Where(x => x.LastName.StartsWith(search) || search == null);
            }
            else {
                records = records.Where(x => x.StaffDOB.StartsWith(search) || search == null);
            }
            switch (sort)
                {
                    case "descending first name":
                        records = records.OrderByDescending(x => x.FirstName);
                        break;

                    case "descending last name":
                        records = records.OrderByDescending(x => x.LastName);
                        break;

                    case "first name":
                        records = records.OrderBy(x => x.FirstName);
                        break;

                    default:
                        records = records.OrderBy(x => x.LastName);
                        break;
                }
            return View(records.ToPagedList(pageNumber ?? 1, 10));
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffId,FirstName,LastName,StaffDOB")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staff);
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,FirstName,LastName,StaffDOB")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staff);
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
