using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GoPlaces.Models
{
    public class Place
    {
        /*
         **************************** Will only need the following 2 properties if I enable Users to add/view Places independently of Adventures...
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        public string UserId { get; set; }
        */

        [Key]
        public int PlaceId{ get; set; }
       
        [Required]
        [StringLength(55)]
        public string Title{ get; set; }
        [StringLength(250)]
        public string Description{ get; set; }

        // AdventureId shouldn't be required if/when Users can access Places independently of Adventures
        [Required]
        public int AdventureId { get; set; }
        public Adventure Adventure { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
