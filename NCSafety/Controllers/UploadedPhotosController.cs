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
    public class UploadedPhotosController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();

        // GET: UploadedPhotos
        public ActionResult Index()
        {
            var uploadedPhotos = db.UploadedPhotos.Include(u => u.Item);
            return View(uploadedPhotos.ToList());
        }

        // GET: UploadedPhotos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadedPhotos uploadedPhotos = db.UploadedPhotos.Find(id);
            if (uploadedPhotos == null)
            {
                return HttpNotFound();
            }
            return View(uploadedPhotos);
        }

        // GET: UploadedPhotos/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.Items, "ID", "itemComment");
            return View();
        }

        // POST: UploadedPhotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FileContent,MimeType,PhotoName,PhotoDescription,ItemID")] UploadedPhotos uploadedPhotos)
        {
            if (ModelState.IsValid)
            {
                db.UploadedPhotos.Add(uploadedPhotos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.Items, "ID", "itemComment", uploadedPhotos.ItemID);
            return View(uploadedPhotos);
        }

        // GET: UploadedPhotos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadedPhotos uploadedPhotos = db.UploadedPhotos.Find(id);
            if (uploadedPhotos == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "itemComment", uploadedPhotos.ItemID);
            return View(uploadedPhotos);
        }

        // POST: UploadedPhotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FileContent,MimeType,PhotoName,PhotoDescription,ItemID")] UploadedPhotos uploadedPhotos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uploadedPhotos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.Items, "ID", "itemComment", uploadedPhotos.ItemID);
            return View(uploadedPhotos);
        }

        // GET: UploadedPhotos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UploadedPhotos uploadedPhotos = db.UploadedPhotos.Find(id);
            if (uploadedPhotos == null)
            {
                return HttpNotFound();
            }
            return View(uploadedPhotos);
        }

        // POST: UploadedPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UploadedPhotos uploadedPhotos = db.UploadedPhotos.Find(id);
            db.UploadedPhotos.Remove(uploadedPhotos);
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
