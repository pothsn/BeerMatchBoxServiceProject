using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class Match
    {
        [Key]
        public string Id { get; set; }

        public string BreweryDBBeerId { get; set; }

        public string Name { get; set; }

        public string BreweryName { get; set; }

        public double? Abv { get; set; }

        public double? Ibu { get; set; }

        public int? GlasswareId { get; set; }

        public string StyleId { get; set; }

        public string StyleName { get; set; }

        public string Description { get; set; }

        public string IsOrganic { get; set; }

        public string IsRetired { get; set; }

        public BreweryDBLabelHolder Images { get; set; }

        //[ForeignKey("BreweryDBBrewery")]
        public string BreweryDBBreweryId { get; set; }
        [NotMapped]
        public BreweryDBBeer BreweryDBBrewery { get; set; }

        public decimal? BeerBreweryLatitude { get; set; }

        public decimal? BeerBreweryLongitude { get; set; }

        public string BeerBreweryAddress { get; set; }

        public string BeerBreweryCity { get; set; }

        public string BeerBreweryState { get; set; }

        public int MatchRating { get; set; }
    }
}
