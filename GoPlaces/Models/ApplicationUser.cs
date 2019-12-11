using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoPlaces.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }

        [Key]
        [Required]
        [Display(Name ="User Handle")]
        public string UserHandle { get; set; }
        public ICollection<Adventure> Adventures { get; set; }
        public bool ShouldGoToLatestAdventure { get; set; }
    }
}
