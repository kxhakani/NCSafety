using NCSafety.DAL.IdentityEntities;
using NCSafety.DAL.NCSafetyEntities;
using NCSafety.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NCSafety.Controllers
{
    [Authorize]
    public class InspectionPagesController : Controller
    {
        private NCSafetyCFEntities db = new NCSafetyCFEntities();
        private ApplicationDbContext dbUsers = new ApplicationDbContext();
        // GET: InspectionPages
        public ActionResult NewInspection()
        {
            PopulateDropDownLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewInspection([Bind(Include = "ID,SchoolID,LabID,inspDate")] Inspection inspection)
        {
            if (ModelState.IsValid)
            {
                db.Inspections.Add(inspection);
                db.SaveChanges();
                if (Request["btn"].Equals("Schedule"))
                    return RedirectToAction("Index", "Inspections", new { area = "" });
                else if (Request["btn"].Equals("Create"))
                    return RedirectToAction("EditInspection", new { date = inspection.inspDate.ToShortDateString(), lab=inspection.LabID, school=inspection.SchoolID, inspection=inspection.ID });
            }

            PopulateDropDownLists();
            return View();
        }

        public ActionResult EditInspection(string date, int? lab, int? school, int? inspection)
        {
            PopulateDropDownLists();

            try
            {
                // get lab name
                var labs = db.Labs.Select(x => x);
                labs = labs.Where(x => x.ID == lab);
                ViewBag.Lab = labs.FirstOrDefault().labFull;
                // get school name
                var schools = db.Schools.Select(x => x);
                schools = schools.Where(x => x.ID == school);
                ViewBag.School = schools.FirstOrDefault().schName;

                ViewBag.Date = date;
                ViewBag.Inspection = inspection;
            }
            catch { }
            
            return View();
        }

        public ActionResult ListInspection()
        {
            
            return View();
        }

        public ActionResult ViewInspection()
        {
            PopulateDropDownLists();
            return View();
        }

        public ActionResult DraftInspection()
        {
            return View();
        }

        public void PopulateDropDownLists(Lab item = null)
        {
            ViewBag.LabID = new SelectList(db.Labs.OrderBy(x => x.labBuilding).ThenBy(x => x.labNumber), "ID", "labFull", item?.ID);
            ViewBag.UserID = new SelectList(dbUsers.Users.OrderBy(x => x.UserName), "ID", "UserName", item?.ID);
            ViewBag.HazardID = new SelectList(db.Hazards.OrderBy(x => x.hazName), "ID", "hazName", item?.ID);
            ViewBag.SchoolID = new SelectList(db.Schools.OrderBy(x => x.schName), "ID", "schName", item?.ID);
        }
    }
}