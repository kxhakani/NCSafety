using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NCSafety.DAL.NCSafetyEntities;
using NCSafety.Models;

namespace NCSafety.Controllers
{
    public class SchoolsController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();

        // GET: Schools
        public ActionResult Index(string SchoolID, string DeanID, string DeanEmailID, string AssistantEmailID)
        {
            PopulateDropDownLists();
            var schools = db.Schools.Select(p => p);

            // Filters
            if (!string.IsNullOrEmpty(SchoolID))
                schools = schools.Where(p => p.schName.Contains(SchoolID));
            if (!string.IsNullOrEmpty(DeanID))
            {
                string[] name = DeanID.Split(null);
                string first = name[0];
                string last = name[1];
                schools = schools.Where(p => p.ascDeanLast == last);
                schools = schools.Where(p => p.ascDeanFirst == first);
            }
            if (!string.IsNullOrEmpty(DeanEmailID))
                schools = schools.Where(p => p.ascDeanEmail == DeanEmailID);
            if (!string.IsNullOrEmpty(AssistantEmailID))
                schools = schools.Where(p => p.ascDeanAssistantEmail == AssistantEmailID);

            return View(schools.ToList());
        }

        // GET: Schools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: Schools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,schName,ascDeanFirst,ascDeanLast,ascDeanEmail,ascDeanAssistantEmail")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        // GET: Schools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,schName,ascDeanFirst,ascDeanLast,ascDeanEmail,ascDeanAssistantEmail")] School school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        // GET: Schools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            School school = db.Schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            School school = db.Schools.Find(id);
            try
            {
                db.Schools.Remove(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "You cannot delete a school that has related objects in the system");
                }
                else
                {
                    ModelState.AddModelError("", "Unable To Save Changes. Try again later. If the problem persists contact your administrator!");
                }
            }
            return View(school);
        }

        private void PopulateDropDownLists(School item = null)
        {
            ViewBag.DeanID = new SelectList(db.Schools.ToList().Select(x => x.ascDeanFullName).Distinct());
            ViewBag.SchoolID = new SelectList(db.Schools.Select(x => x.schName).Distinct());
            ViewBag.DeanEmailID = new SelectList(db.Schools.Select(x => x.ascDeanEmail).Distinct());
            ViewBag.AssistantEmailID = new SelectList(db.Schools.Select(x => x.ascDeanAssistantEmail).Distinct());
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
