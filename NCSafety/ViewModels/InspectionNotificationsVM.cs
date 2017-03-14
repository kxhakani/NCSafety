using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.ViewModels
{
    public class InspectionNotificationsVM
    {
        public int ID { get; set; }

        [Display(Name = "Inspection")]
        public int InspectionID { get; set; }

        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "You cannot leave the corrective action due date empty")]
        public DateTime itemCorrActionDue { get; set; }

        [Display(Name = "Completion Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? itemCorrActionCompleted { get; set; }

    }
}