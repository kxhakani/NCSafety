using System;
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

        [Display(Name ="Inspection")]
        public int InspectionID { get; set; }

        [Display(Name ="Good")]
        public bool isGood { get; set; }

        [Display(Name ="Fault")]
        public bool isFault { get; set; }

        [Display(Name = "Due Date")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        [Required(ErrorMessage = "You cannot leave the corrective action due date empty")]
        public DateTime itemCorrActionDue { get; set; }

        [Display(Name = "Completion Date")]
        [DisplayFormat(DataFormatString ="{0:d}")]
        public DateTime? itemCorrActionCompleted { get; set; }

        [Display(Name = "Comment")]
        public string itemComment { get; set; }

        public virtual Inspection Inspection { get; set; }

        public virtual Hazard Hazard { get; set; }

        public virtual Equipment Equipment { get; set; }

        public ICollection<UploadedPhotos> UploadedPhotos { get; set; }

        //constructor
        public Item ()
        {
            this.UploadedPhotos = new HashSet<UploadedPhotos>();
        }

        [ScaffoldColumn(false)]
        public byte[] imageContent { get; set; }

        [StringLength(256)]
        [ScaffoldColumn(false)]
        public string imageMimeType { get; set; }

        [StringLength(100, ErrorMessage = "The name cannot be more than 100 characters")]
        [Display(Name = "Picture File")]
        [ScaffoldColumn(false)]
        public string imageFileName { get; set; }


    }

}