using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BigProject.Models
{
    public class ContactModel
    {
        [Required]
        [MaxLength(50, ErrorMessage ="Too Long")]
        public String Name { set; get; }
        [Required]
        public String Password { set; get; }
        [Required]
        [EmailAddress]
        public String Email { set; get; }
    }
}
