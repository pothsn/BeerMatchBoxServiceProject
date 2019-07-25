using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class UserBeer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        //Fake-ish foreign key to BreweryDB API
        public string BreweryDBBeerId { get; set; }
        public BreweryDBBeer BreweryDBBeer { get; set; }

        //Fake-ish foreign key to BreweryDB API
        public string BreweryDBBreweryId { get; set; }
        public BreweryDBBrewery BreweryDBBrewery { get; set; }
    }
}
