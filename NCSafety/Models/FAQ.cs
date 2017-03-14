using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.Models
{
    public class FAQ
    {
        public int ID { get; set; }

        [Display(Name = "Question")]
        [Required(ErrorMessage = "You cannot leave the question empty.")]
        public string faqQuestion { get; set; }

        [Display(Name = "Answer")]
        [Required(ErrorMessage = "You cannot leave the answer empty.")]
        public string faqAnswer { get; set; }
    }
}