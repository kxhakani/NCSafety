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
using NCSafety.ViewModels;

namespace NCSafety.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();

        // GET: Reports
        public ActionResult Index(string HazardID)
        {
            PopulateDropDownLists();
            var hazards = db.Hazards.Select(p => p);

            // Filters
            if (!string.IsNullOrEmpty(HazardID))
                hazards = hazards.Where(p => p.hazName.Contains(HazardID));

            //if (!string.IsNullOrEmpty(DeanID))
            //{
            //    string[] name = DeanID.Split(null);
            //    string first = name[0];
            //    string last = name[1];
            //    schools = schools.Where(p => p.ascDeanLast == last);
            //    schools = schools.Where(p => p.ascDeanFirst == first);
            //}
            //if (!string.IsNullOrEmpty(DeanEmailID))
            //    schools = schools.Where(p => p.ascDeanEmail == DeanEmailID);
            //if (!string.IsNullOrEmpty(AssistantEmailID))
            //    schools = schools.Where(p => p.ascDeanAssistantEmail == AssistantEmailID);

            return View();
        }

        // GET: Reports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Reports/Create
        public ActionResult Create()
        {
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,itemArea,itemHazComment,itemCorrActionDt,itemCorrActionCompleted,itemComment,InspectionID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
            return View(item);
        }

        // GET: Reports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
            return View(item);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,itemArea,itemHazComment,itemCorrActionDt,itemCorrActionCompleted,itemComment,InspectionID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
            return View(item);
        }

        // GET: Reports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult InProgress()
        {
            var inspectionReportsVM = new InspectionReportsVM();
            return View(inspectionReportsVM);
        }

        public ActionResult CorrectiveActionsIndex()
        {
            var correctiveActionReportsVM = new CorrectiveActionReportsVM();
            return View(correctiveActionReportsVM);
        }


        private void PopulateDropDownLists(Item item = null)
        {
            ViewBag.HazardsID = new SelectList(db.Items.OrderBy(h => h.itemCorrActionDue).ThenBy(h => h.itemCorrActionCompleted), "ascDeanFullName", "ascDeanFullName", item?.ID);
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
