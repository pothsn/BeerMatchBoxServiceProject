using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class GetMatchesViewModel
    {
        public List<Match> Matches { get; set; }

        public UserTaste UserTaste { get; set; }

        public List<DoughnutSection> DoughnutSections { get; set; }
    }
}
