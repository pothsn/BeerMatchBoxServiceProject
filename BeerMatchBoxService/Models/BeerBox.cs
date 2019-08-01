using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class BeerBox
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public string BeerOneBreweryDBId { get; set; }
        public string BeerOneName { get; set; }

        public string BeerTwoBreweryDBId { get; set; }
        public string BeeTwoName { get; set; }

        public string BeerThreeBreweryDBId { get; set; }
        public string BeerThreeName { get; set; }

        public string BeerFourBreweryDBId { get; set; }
        public string BeerFourName { get; set; }

        public string BeerFiveBreweryDBId { get; set; }
        public string BeerFiveName { get; set; }
    }
}
