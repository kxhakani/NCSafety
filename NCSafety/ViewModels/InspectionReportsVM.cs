using NCSafety.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.ViewModels
{
    public class InspectionReportsVM
    {
        public int ID { get; set; }

        [Display(Name = "Hazard")]
        public int HazardID { get; set; }

        [Display(Name = "Equipment")]
        public int EquipmentID { get; set; }

        [Display(Name = "Inspection")]
        public int InspectionID { get; set; }

        [Display(Name = "Good")]
        public bool isGood { get; set; }

        [Display(Name = "Fault")]
        public bool isFault { get; set; }

        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "You cannot leave the corrective action due date empty")]
        public DateTime itemCorrActionDue { get; set; }

        [Display(Name = "Completion Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? itemCorrActionCompleted { get; set; }

        [Display(Name = "Comment")]
        public string itemComment { get; set; }

        [Display(Name = "School")]
        [Required(ErrorMessage = "You must select a school.")]
        public int SchoolID { get; set; }

        [Display(Name = "Lab")]
        [Required(ErrorMessage = "You must select a lab.")]
        public int LabID { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "You cannot leave the inspection date empty")]
        public DateTime inspDate { get; set; }

        [Display(Name = "Inspector")]
        [Required(ErrorMessage = "You must select an Inspector.")]
        public string inspector { get; set; }

        public virtual Hazard Hazard { get; set; }

        public ICollection<UploadedPhotos> UploadedPhotos { get; set; }

    }
}