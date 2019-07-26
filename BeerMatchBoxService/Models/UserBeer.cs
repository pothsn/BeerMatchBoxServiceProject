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

        public string StyleName { get; set; }

        public int StyleId { get; set; }

        public double? Abv { get; set; }
        public double? Ibu { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("BreweryDBBeer")]
        public string BreweryDBBeerId { get; set; }
        public BreweryDBBeer BreweryDBBeer { get; set; }

        //[ForeignKey("BrweryDBBrewery")]
        //public string BreweryDBBreweryId { get; set; }
        //public BreweryDBBrewery BreweryDBBrewery { get; set; }
    }
}
