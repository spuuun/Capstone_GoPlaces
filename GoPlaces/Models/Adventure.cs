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

        //public int? PlaceId { get; set; }

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
    
        public bool IsPublic { get; set; }

        public List<Place> Places { get; set; }
    
    //LOCATION INFORMATION???
    }
}
