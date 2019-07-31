using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class BreweryDBBrewery
    {
        [Key]
        public string Id { get; set; }

        public string BreweryDBBreweryId { get; set; }

        public string Name { get; set; }

        public string NameShortDisplay { get; set; }

        public string Description { get; set; }

        public string Website { get; set; }

        public int Established { get; set; }

        public string IsOrganic { get; set; }

        public BreweryDBIconHolder Images { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }





    }
}
