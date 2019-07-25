using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class BreweryDBLabelHolder
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BreweryDBBeer")]
        public string BreweryDBBeerId { get; set; }
        public BreweryDBBeer BreweryDBBeer { get; set; }

        public string Icon { get; set; }

        public string Medium { get; set; }

        public string Large { get; set; }

        public string ContentAwareIcon { get; set; }

        public string ContentAwareMedium { get; set; }

        public string ContentAwareLarge { get; set; }



    }
}
