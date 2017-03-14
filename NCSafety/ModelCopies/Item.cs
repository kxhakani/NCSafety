﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.Models
{
    public class Item
    {
        public int ID { get; set; }

        [Display(Name ="Hazard")]
        public int HazardID { get; set; }

        [Display(Name ="Equipment")]
        public int EquipmentID { get; set; }

        public int InspectionID { get; set; }

        [Display(Name ="Good")]
        public bool isGood { get; set; }

        [Display(Name ="Fault")]
        public bool isFault { get; set; }

        [Display(Name = "Due Date")]
        [Required(ErrorMessage = "You cannot leave the corrective action due date empty")]
        public DateTime itemCorrActionDue { get; set; }

        [Display(Name = "Completion Date")]
        public DateTime? itemCorrActionCompleted { get; set; }

        [Display(Name = "Comment")]
        public string itemComment { get; set; }

        public virtual Inspection Inspection { get; set; }

        public virtual Hazard Hazard { get; set; }

        public virtual Equipment Equipment { get; set; }

    }
}