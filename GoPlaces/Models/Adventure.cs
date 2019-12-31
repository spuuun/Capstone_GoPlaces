using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoPlaces.Models
{
    public class Adventure
    {
        [Key]
        public int AdventureId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        
        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Public")]
        public bool IsPublic { get; set; }

        public List<Place> Places { get; set; }

        // FOR LINK ON LOGIN PAGE: 'take me to my current adventure'
        //public bool IsActive { get; set; }

        // DIFFERENT THAN TITLE --- may improve UX, but not sure if it's necessary.
        //public string LocationName { get; set; }
    }
}
