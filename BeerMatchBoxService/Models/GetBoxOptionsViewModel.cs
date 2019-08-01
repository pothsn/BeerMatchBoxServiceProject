using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class GetBoxOptionsViewModel
    {
        public List<Match> PreciseMatch { get; set; }

        public List<Match> SomethingDifferent { get; set; }

    }
}
