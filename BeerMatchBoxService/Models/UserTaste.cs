using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BeerMatchBoxService.Models
{
    public class UserTaste
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        //QUIZ BELOW! These will be: Strongly disagree (1) / disagree (2) / Indifferent or I don't know (3) / agree (4) / strongly agree (5)
        [Display(Name ="I do not consider myself to be overly sensitive to bitterness.")]
        [Range(typeof(int), "0", "5")]
        public int LikesBitter { get; set; }

        [Display(Name = "I like like fruit, drinks or candy with a sour taste.")]
        [Range(typeof(int), "0", "5")]
        public int LikesSour { get; set; }

        [Display(Name = "I like the taste and aroma of hops.")]
        [Range(typeof(int), "0", "5")]
        public int LikesHoppy { get; set; }

        [Display(Name = "I like chocolate!")]
        [Range(typeof(int), "0", "5")]
        public int LikesMalty { get; set; }

        [Display(Name = "I gravitate towards sweet things.")]
        [Range(typeof(int), "0", "5")]
        public int LikesSweet { get; set; }

        [Display(Name = "I like special beers that often come with a higher ABV.")]
        [Range(typeof(int), "0", "5")]
        public int LikesStrong { get; set; }

        [Display(Name = "I mind the ABV of beer and prefer something more sessionable.")]
        [Range(typeof(int), "0", "5")]
        public int LikesSession { get; set; }

        [Display(Name = "I like organic beer.")]
        [Range(typeof(int), "0", "5")]
        public int LikesOrganic { get; set; }

        [Display(Name = "I enjoy Pale Ales.")]
        [Range(typeof(int), "0", "5")]
        public int LikesPaleAle { get; set; }

        [Display(Name = "I enjoy IPAs.")]
        [Range(typeof(int), "0", "5")]
        public int LikesIPA { get; set; }

        [Display(Name = "I enjoy West Coast IPAs.")]
        [Range(typeof(int), "0", "5")]
        public int LikesWestCoastIPA { get; set; }

        [Display(Name = "I enjoy New Englad IPAs.")]
        [Range(typeof(int), "0", "5")]
        public int LikesNewEnglandIPA { get; set; }

        [Display(Name = "I enjoy Stouts.")]
        [Range(typeof(int), "0", "5")]
        public int LikesStout { get; set; }

        [Display(Name = "I enjoy Porters.")]
        [Range(typeof(int), "0", "5")]
        public int LikesPorter { get; set; }

        [Display(Name = "I enjoy Brown Ales.")]
        [Range(typeof(int), "0", "5")]
        public int LikesBrownAle { get; set; }

        [Display(Name = "I enjoy Ambers.")]
        [Range(typeof(int), "0", "5")]
        public int LikesAmber { get; set; }

        [Display(Name = "I enjoy Red Ales.")]
        [Range(typeof(int), "0", "5")]
        public int LikesRedAle { get; set; }

        [Display(Name = "I enjoy Wheat Beers.")]
        [Range(typeof(int), "0", "5")]
        public int LikesWheat { get; set; }

        [Display(Name = "I enjoy Hefeweizens.")]
        [Range(typeof(int), "0", "5")]
        public int LikesHefeweizen { get; set; }

        [Display(Name = "I enjoy wheat Belgian Witbiers.")]
        [Range(typeof(int), "0", "5")]
        public int LikesBelgianWit { get; set; }

        [Display(Name = "I enjoy Sours.")]
        [Range(typeof(int), "0", "5")]
        public int LikesSourBeer { get; set; }

        [Display(Name = "I enjoy Wild Ales.")]
        [Range(typeof(int), "0", "5")]
        public int LikesWildSour { get; set; }

        [Display(Name = "I enjoy Berliner Weisses.")]
        [Range(typeof(int), "0", "5")]
        public int LikesBerlinerWeisseSour { get; set; }

        [Display(Name = "I enjoy Goses.")]
        [Range(typeof(int), "0", "5")]
        public int LikesGoseSour { get; set; }

        [Display(Name = "I enjoy Saisons.")]
        [Range(typeof(int), "0", "5")]
        public int LikesSaison { get; set; }

        [Display(Name = "I enjoy Belgian Doubles/Trippels.")]
        [Range(typeof(int), "0", "5")]
        public int LikesBelgianDoubleTrippel { get; set; }
    }
}
