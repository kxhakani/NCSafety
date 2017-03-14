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
    public class NotificationsController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();

        // GET: Notifications
        public ActionResult Index()
        {
            var correctiveActionNotificationsVM = new CorrectiveActionNotificationsVM();
            return View(correctiveActionNotificationsVM);

            //var items = db.Items.Include(i => i.Inspection);
            //return View(items.ToList());
        }

        // GET: Notifications/Details/5
        public ActionResult Details()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Item item = db.Items.Find(id);
            //if (item == null)
            //{
            //    return HttpNotFound();
            //}
            var correctiveActionNotificationsVM = new CorrectiveActionNotificationsVM();
            return View(correctiveActionNotificationsVM);
        }

        public ActionResult InspectionDetails()
        {
            var correctiveActionNotificationsVM = new CorrectiveActionNotificationsVM();
            return View(correctiveActionNotificationsVM);
        }

        public ActionResult AllDetails()
        {
            var correctiveActionNotificationsVM = new CorrectiveActionNotificationsVM();
            return View(correctiveActionNotificationsVM);
        }

        //// GET: Notifications/Create
        //public ActionResult Create()
        //{
        //    ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID");
        //    return View();
        //}

        //// POST: Notifications/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,itemArea,itemHazComment,itemCorrActionDt,itemCorrActionCompleted,itemComment,InspectionID")] Item item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Items.Add(item);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
        //    return View(item);
        //}

        //// GET: Notifications/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Item item = db.Items.Find(id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
        //    return View(item);
        //}

        //// POST: Notifications/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,itemArea,itemHazComment,itemCorrActionDt,itemCorrActionCompleted,itemComment,InspectionID")] Item item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(item).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
        //    return View(item);
        //}

        //// GET: Notifications/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Item item = db.Items.Find(id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(item);
        //}

        //// POST: Notifications/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Item item = db.Items.Find(id);
        //    db.Items.Remove(item);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //ButtonClick Event for Corrective Actions Past Due
        [HttpGet]
        public ViewResult CorrectiveActions()
        {
            var correctiveActionNotificationsVM = new CorrectiveActionNotificationsVM();
            return View(correctiveActionNotificationsVM);
        }

        [HttpPost]
        public ActionResult CorrectiveAction(CorrectiveActionNotificationsVM model)
        {
            return RedirectToAction("Success");
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
