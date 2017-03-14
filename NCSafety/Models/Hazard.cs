using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.Models
{
    public class Hazard
    {
        public int ID { get; set; }

        [Display(Name = "Hazard")]
        [Required(ErrorMessage = "You cannot leave the hazard name blank")]
        public string hazName { get; set; }

        [Display(Name = "Description")]
        public string hazDescription { get; set; }

        public virtual ICollection<Lab> Labs { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}