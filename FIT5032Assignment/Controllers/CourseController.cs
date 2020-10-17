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
    public class CourseController : Controller
    {
        private NextGenDatabaseEntities db = new NextGenDatabaseEntities();

        // GET: Course
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Language).Include(c => c.Staff);
            return View(courses.ToList());
        }
        public ActionResult Display(string option, string search, int? pageNumber, string sort)
        {
            ViewBag.SortByCourseName = string.IsNullOrEmpty(sort) ? "descending course name" : "";
            ViewBag.SortByLanguageName = sort == "LanguageName" ? "descending language name" : "LanguageName";
            var records = db.Courses.AsQueryable();
            if (option == "CourseName")
            {
                records = records.Where(x => x.CourseName.StartsWith(search) || search == null);
            }
            else if (option == "LanguageName")
            {
                records = records.Where(x => x.Language.LanguageName.StartsWith(search) || search == null);
            }
            else
            {
                records = records.Where(x => x.Staff.FirstName.StartsWith(search) || search == null);
            }
            switch (sort)
            {
                case "descending course name":
                    records = records.OrderByDescending(x => x.CourseName);
                    break;

                case "descending language name":
                    records = records.OrderByDescending(x => x.Language.LanguageName);
                    break;

                case "LanguageName":
                    records = records.OrderBy(x => x.Language.LanguageName);
                    break;

                default:
                    records = records.OrderBy(x => x.CourseName);
                    break;
            }
            return View(records.ToPagedList(pageNumber ?? 1, 10));
        }
        // GET: Course/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName");
            ViewBag.Staffid = new SelectList(db.Staffs, "StaffId", "FirstName");
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,Staffid,LanguageId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", course.LanguageId);
            ViewBag.Staffid = new SelectList(db.Staffs, "StaffId", "FirstName", course.Staffid);
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", course.LanguageId);
            ViewBag.Staffid = new SelectList(db.Staffs, "StaffId", "FirstName", course.Staffid);
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,Staffid,LanguageId")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "LanguageId", "LanguageName", course.LanguageId);
            ViewBag.Staffid = new SelectList(db.Staffs, "StaffId", "FirstName", course.Staffid);
            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
