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
using System.IO;

namespace NCSafety.Controllers
{
    public class ItemsController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();

        // GET: Items
        public ActionResult Index(string HazardID, int? InspectionID, bool? isGood, bool? isFault, string InspDateID, string DueDateID, string CompDateID, string SearchString, string FromDateID, string ToDateID)
        {
            PopulateDropDownLists();
            var items = db.Items.Include(i => i.Equipment).Include(i => i.Hazard).Include(i => i.Inspection);

            // Filters
            if (!string.IsNullOrEmpty(HazardID))
                items = items.Where(p => p.Hazard.hazName.Contains(HazardID));
            if (InspectionID.HasValue)
                items = items.Where(p => p.InspectionID == InspectionID);
            if (!string.IsNullOrEmpty(InspDateID))
            {
                DateTime date = DateTime.Parse(InspDateID);
                items = items.Where(p => p.Inspection.inspDate == date);
            }
            if (!string.IsNullOrEmpty(FromDateID) && !string.IsNullOrEmpty(ToDateID))
            {
                DateTime from = DateTime.Parse(FromDateID);
                DateTime to = DateTime.Parse(ToDateID);
                items = items.Where(p => p.Inspection.inspDate >= from);
                items = items.Where(p => p.Inspection.inspDate <= to);
            }
            if (!string.IsNullOrEmpty(DueDateID))
            {
                DateTime date = DateTime.Parse(DueDateID);
                items = items.Where(p => p.itemCorrActionDue == date);
            }
            if (!string.IsNullOrEmpty(CompDateID))
            {
                DateTime date = DateTime.Parse(CompDateID);
                items = items.Where(p => p.itemCorrActionCompleted == date);
            }
            if (isGood != null && isGood == false)
                items = items.Where(p => p.isGood != true);
            if (isFault != null && isFault == false)
                items = items.Where(p => p.isFault != true);
            if (!string.IsNullOrEmpty(SearchString))
                items = items.Where(p => p.itemComment.Contains(SearchString));

            return View(items.ToList());
        }

        // GET: Items/Details/5
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

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentID = new SelectList(db.Equipments, "ID", "eqName");
            ViewBag.HazardID = new SelectList(db.Hazards, "ID", "hazName");
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,HazardID,EquipmentID,InspectionID,isGood,isFault,itemCorrActionDue,itemCorrActionCompleted,itemComment,imageContent,imageMimeType,imageFileName")] Item item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //code to add the uploaded picture
                    HttpPostedFileBase f = Request.Files["thePicture"];
                    string mimeType = f.ContentType;
                    int fileLength = f.ContentLength;
                    if (!(mimeType == "" || fileLength == 0))
                    {
                        string fileName = Path.GetFileName(f.FileName);
                        Stream fileStream = f.InputStream;
                        byte[] fileData = new byte[fileLength];
                        fileStream.Read(fileData, 0, fileLength);

                        if (mimeType.Contains("image"))
                        {
                            item.imageContent = fileData;
                            item.imageMimeType = mimeType;
                            item.imageFileName = fileName;
                        }

                    }
                    db.Items.Add(item);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable To Save Changes. Try again later. If the problem persists contact your administrator!");
            }

            ViewBag.EquipmentID = new SelectList(db.Equipments, "ID", "eqName", item.EquipmentID);
            ViewBag.HazardID = new SelectList(db.Hazards, "ID", "hazName", item.HazardID);
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
            return View(item);
        }

        // GET: Items/Edit/5
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
            ViewBag.EquipmentID = new SelectList(db.Equipments, "ID", "eqName", item.EquipmentID);
            ViewBag.HazardID = new SelectList(db.Hazards, "ID", "hazName", item.HazardID);
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int ?id/*[Bind(Include = "ID,HazardID,EquipmentID,InspectionID,isGood,isFault,itemCorrActionDue,itemCorrActionCompleted,itemComment,imageContent,imageMimeType,imageFileName")] Item item*/, string chkRemoveImage)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item itemToUpdate = db.Items.Find(id);

            if (TryUpdateModel(itemToUpdate, "", new string[] { "isGood", "isFault", "itemCorrActionDue", "itemCorrActionCompleted", "itemComment", "imageContent", "imageMimeType", "imageFileName" }))
            {
                try
                {
                    if (chkRemoveImage != null)
                    {
                        itemToUpdate.imageContent = null;
                        itemToUpdate.imageMimeType = null;
                        itemToUpdate.imageFileName = null;
                    }

                    HttpPostedFileBase f = Request.Files["newPicture"];
                    string mimeType = f.ContentType;
                    int fileLength = f.ContentLength;
                    if (!(mimeType=="" || fileLength ==0))
                    {
                        string fileName = Path.GetFileName(f.FileName);
                        Stream fileStream = f.InputStream;
                        byte[] fileData = new byte[fileLength];
                        fileStream.Read(fileData, 0, fileLength);

                        if (mimeType.Contains("image"))
                        {
                            itemToUpdate.imageContent = fileData;
                            itemToUpdate.imageMimeType = mimeType;
                            itemToUpdate.imageFileName = fileName;
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(DataException)
                {
                    ModelState.AddModelError("", "Unable To Save Changes. Try again later. If the problem persists contact your administrator!");
                }
            }

            ViewBag.EquipmentID = new SelectList(db.Equipments, "ID", "eqName", itemToUpdate.EquipmentID);
            ViewBag.HazardID = new SelectList(db.Hazards, "ID", "hazName", itemToUpdate.HazardID);
            ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", itemToUpdate.InspectionID);
            return View(itemToUpdate);

            //if (ModelState.IsValid)
            //{
            //    if(chkRemoveImage != null)
            //    {

                //    }
                //    db.Entry(item).State = EntityState.Modified;
                //    db.SaveChanges();
                //    return RedirectToAction("Index");
                //}
                //ViewBag.EquipmentID = new SelectList(db.Equipments, "ID", "eqName", item.EquipmentID);
                //ViewBag.HazardID = new SelectList(db.Hazards, "ID", "hazName", item.HazardID);
                //ViewBag.InspectionID = new SelectList(db.Inspections, "ID", "ID", item.InspectionID);
                //return View(item);
        }

        // GET: Items/Delete/5
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

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            try
            {
                db.Items.Remove(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(DataException dex)
            {
                if (dex.InnerException.InnerException.Message.Contains("Fk_"))
                {
                    ModelState.AddModelError("", "You cannot delete an item that has related objects in the system");
                }
                else
                {
                    ModelState.AddModelError("", "Unable To Save Changes. Try again later. If the problem persists contact your administrator!");
                }
            }
            return View(item);
        }

        private void PopulateDropDownLists(Item item = null)
        {
            ViewBag.HazardID = new SelectList(db.Hazards.Select(x => x.hazName).Distinct());
            ViewBag.InspectionID = new SelectList(db.Inspections
                .OrderBy(i => i.ID), "ID", "ID", item?.InspectionID);
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
