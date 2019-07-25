using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class BreweryDBIconHolder
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BreweryDBBrewery")]
        public string BreweryDBBreweryId { get; set; }
        public BreweryDBBrewery BreweryDBBrewery { get; set; }

        public string Icon { get; set; }

        public string Medium { get; set; }

        public string Large { get; set; }

        public string SquareMedium { get; set; }

        public string SquareLarge { get; set; }
    }
}
