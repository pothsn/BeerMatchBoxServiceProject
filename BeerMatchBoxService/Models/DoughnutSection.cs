using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class DoughnutSection
    {
        [Key]
        public int Id { get; set; }

        public string StyleId { get; set; }

        public string StyleName { get; set; }

        public int Percentage { get; set; }
    }
}
