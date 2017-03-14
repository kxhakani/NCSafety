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
    public class LabsController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();

        // GET: Labs
        public ActionResult Index(int? LabID, string SchoolID, string BuildingID, string NumberID)
        {
            PopulateDropDownLists();

            var labs = db.Labs.Select(x=>x);

            // Filters
            if (LabID.HasValue)
                labs = labs.Where(p => p.ID == LabID);
            if (!string.IsNullOrEmpty(SchoolID))
                labs = labs.Where(p => p.Schools.Any(x => x.schName == SchoolID));
            if (!string.IsNullOrEmpty(BuildingID))
                labs = labs.Where(p => p.labBuilding == BuildingID);
            if (!string.IsNullOrEmpty(NumberID))
                labs = labs.Where(p => p.labNumber == NumberID);

            return View(labs.ToList());
        }

        // GET: Labs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        // GET: Labs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Labs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,labBuilding,labNumber")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                db.Labs.Add(lab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lab);
        }

        // GET: Labs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        // POST: Labs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,labBuilding,labNumber")] Lab lab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lab);
        }

        // GET: Labs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lab lab = db.Labs.Find(id);
            if (lab == null)
            {
                return HttpNotFound();
            }
            return View(lab);
        }

        // POST: Labs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lab lab = db.Labs.Find(id);
            db.Labs.Remove(lab);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateDropDownLists(Lab item = null)
        {
            // Lab ddl
            ViewBag.LabID = new SelectList(db.Labs
                .OrderBy(h => h.labBuilding).ThenBy(h=>h.labNumber), "ID", "labFull", item?.ID);
            // School ddl
            ViewBag.SchoolID = new SelectList(db.Schools.Select(x => x.schName).Distinct());

            // Building ddl
            List<string> buildings = new List<string>();
            foreach(Lab l in db.Labs)
            {
                if (!buildings.Contains(l.labBuilding))
                    buildings.Add(l.labBuilding);
            }
            buildings.Sort();
            ViewBag.BuildingID = new SelectList(buildings);

            // Number ddl
            List<string> numbers = new List<string>();
            foreach(Lab l in db.Labs)
            {
                if (!numbers.Contains(l.labNumber))
                    numbers.Add(l.labNumber);
            }
            numbers.Sort();
            ViewBag.NumberID = new SelectList(numbers);

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
