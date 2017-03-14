using NCSafety.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.ViewModels
{
    public class CorrectiveActionReportsVM
    {
        public int ID { get; set; }

        [Display(Name = "Hazard")]
        public int HazardID { get; set; }

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

        [Display(Name = "Hazard")]
        [Required(ErrorMessage = "You cannot leave the hazard name blank")]
        public string hazName { get; set; }

        [Display(Name = "Description")]
        public string hazDescription { get; set; }

        public virtual Inspection Inspection { get; set; }

        public virtual Hazard Hazard { get; set; }

        public virtual Equipment Equipment { get; set; }

        //internal static object ToList()
        //{
        //    throw new NotImplementedException();
        //}

        public ICollection<UploadedPhotos> UploadedPhotos { get; set; }

    }
}