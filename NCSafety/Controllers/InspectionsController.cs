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
    public class InspectionsController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();

        // GET: Inspections
        public ActionResult Index(int? LabID, string SchoolID, string FromDateID, string ToDateID, string DateID, int? InspectionID)
        {
            PopulateDropDownLists();
            var inspections = db.Inspections.Include(i => i.Lab).Include(i => i.School);

            // Filters
            if (InspectionID.HasValue)
                inspections = inspections.Where(p => p.ID == InspectionID);
            if (LabID.HasValue)
                inspections = inspections.Where(p => p.LabID == LabID);
            if (!string.IsNullOrEmpty(SchoolID))
                inspections = inspections.Where(p => p.School.schName.Contains(SchoolID));
            if (!string.IsNullOrEmpty(DateID))
            {
                DateTime date = DateTime.Parse(DateID);
                inspections = inspections.Where(p => p.inspDate == date);
            }
            if (!string.IsNullOrEmpty(FromDateID) && !string.IsNullOrEmpty(ToDateID))
            {
                DateTime FromDate = DateTime.Parse(FromDateID);
                DateTime toDate = DateTime.Parse(ToDateID);
                inspections = inspections.Where(p => p.inspDate >= FromDate);
                inspections = inspections.Where(p => p.inspDate <= toDate);
            }

            return View(inspections.ToList());
        }

        // GET: Inspections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // GET: Inspections/Create
        public ActionResult Create()
        {
            PopulateDropDownLists();
            //ViewBag.LabID = new SelectList(db.Labs, "ID", "labBuilding");
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "schName");
            return View();
        }

        // POST: Inspections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SchoolID,LabID,inspDate")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Inspections.Add(inspection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateDropDownLists();
            //ViewBag.LabID = new SelectList(db.Labs, "ID", "labBuilding", inspection.LabID);
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "schName", inspection.SchoolID);
            return View(inspection);
        }

        // GET: Inspections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.LabID = new SelectList(db.Labs, "ID", "labBuilding", inspection.LabID);
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "schName", inspection.SchoolID);
            return View(inspection);
        }

        // POST: Inspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SchoolID,LabID,inspDate")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inspection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LabID = new SelectList(db.Labs, "ID", "labBuilding", inspection.LabID);
            ViewBag.SchoolID = new SelectList(db.Schools, "ID", "schName", inspection.SchoolID);
            return View(inspection);
        }

        // GET: Inspections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inspection inspection = db.Inspections.Find(id);
            if (inspection == null)
            {
                return HttpNotFound();
            }
            return View(inspection);
        }

        // POST: Inspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inspection inspection = db.Inspections.Find(id);
            db.Inspections.Remove(inspection);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulateDropDownLists(Inspection item = null)
        {
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item?.ID);
            ViewBag.LabID = new SelectList(db.Labs
               .OrderBy(h => h.labBuilding).ThenBy(h => h.labNumber), "ID", "labFull", item?.ID);
            ViewBag.SchoolID = new SelectList(db.Schools.Select(x => x.schName).Distinct());
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
