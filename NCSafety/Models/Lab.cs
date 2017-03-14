using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.Models
{
    public class Lab
    {
        public int ID { get; set; }

        [Display(Name = "Building")]
        [Required(ErrorMessage = "You cannot leave the lab building empty")]
        public string labBuilding { get; set; }

        [Display(Name = "Number")]
        [Required(ErrorMessage = "You cannot leave the lab number empty")]
        public string labNumber { get; set; }

        [Display(Name ="Lab")]
        public string labFull
        {
            get
            {
                return labBuilding + labNumber;
            }
        }

        public virtual ICollection<Hazard> Hazards { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }

        public virtual ICollection<School> Schools { get; set; }
    }
}