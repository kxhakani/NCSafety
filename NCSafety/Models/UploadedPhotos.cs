using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NCSafety.Models
{
    public class UploadedPhotos
    {
        public int ID { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        [Required]
        [StringLength(256)]
        public string MimeType { get; set; }

        [Required(ErrorMessage = "You cannot leave the name blank!")]
        [StringLength(255, ErrorMessage = "The name cannot be more than 255 characters!")]
        [Display (Name = "Name")]
        public string PhotoName { get; set; }

        [StringLength(100, ErrorMessage = "The photo description cannot be more than 100 characters!")]
        [Display(Name = "Description")]
        public string PhotoDescription { get; set; }

        public int ItemID { get; set; }

        public virtual Item Item { get; set; }
    }
}