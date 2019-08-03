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
        //Flavor specific
        [Display(Name ="I do not consider myself to be overly sensitive to bitterness")]
        [Range(typeof(int), "0", "10")]
        public int LikesBitter { get; set; }

        [Display(Name = "I like like things with a fruity taste")]
        [Range(typeof(int), "0", "10")]
        public int LikesFruity { get; set; }

        [Display(Name = "I like like things with a sour taste")]
        [Range(typeof(int), "0", "10")]
        public int LikesSour { get; set; }

        [Display(Name = "I like the taste and aroma of hops")]
        [Range(typeof(int), "0", "10")]
        public int LikesHoppy { get; set; }

        //New
        [Display(Name = "I like maltiness")]
        [Range(typeof(int), "0", "10")]
        public int LikesMalty { get; set; }

        //Changed/New
        [Display(Name = "I like chocolate")]
        [Range(typeof(int), "0", "10")]
        public int LikesChocolate { get; set; }

        //New
        [Display(Name = "I like Coffee")]
        [Range(typeof(int), "0", "10")]
        public int LikesCoffee { get; set; }

        [Display(Name = "I gravitate towards sweet things (not only considering beer)")]
        [Range(typeof(int), "0", "10")]
        public int LikesSweet { get; set; }

        [Display(Name = "I like special beers that often come with a higher ABV")]
        [Range(typeof(int), "0", "10")]
        public int LikesStrong { get; set; }

        [Display(Name = "I mind the ABV of beer and prefer something more sessionable")]
        [Range(typeof(int), "0", "10")]
        public int LikesSession { get; set; }

        //[Display(Name = "I like organic beer.")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesOrganic { get; set; }

        //Coloration specific
        //New
        [Display(Name = "I enjoy pale beers (of straw or golden color)")]
        [Range(typeof(int), "0", "10")]
        public int LikesPale { get; set; }

        //New
        [Display(Name = "I enjoy beers of a middling color (of amber, brown or red)")]
        [Range(typeof(int), "0", "10")]
        public int LikesMiddling { get; set; }

        //New
        [Display(Name = "I enjoy dark beers (of dark brown, dark red, black or any other deep shade)")]
        [Range(typeof(int), "0", "10")]
        public int LikesDark { get; set; }

        //New
        [Display(Name = "I enjoy barrel-aged beers")]
        [Range(typeof(int), "0", "10")]
        public int LikesBarrelAged { get; set; }



        //Style specific
        //New
        [Display(Name = "I enjoy Lagers")]
        [Range(typeof(int), "0", "10")]
        public int LikesLager { get; set; }

        //New
        [Display(Name = "I enjoy Ales")]
        [Range(typeof(int), "0", "10")]
        public int LikesAle { get; set; }

        [Display(Name = "I enjoy Pale Ales")]
        [Range(typeof(int), "0", "10")]
        public int LikesPaleAle { get; set; }

        [Display(Name = "I enjoy India  Pale Ales")]
        [Range(typeof(int), "0", "10")]
        public int LikesIPA { get; set; }

        //New
        [Display(Name = "I enjoy Extra Special Bitters")]
        [Range(typeof(int), "0", "10")]
        public int LikesESB { get; set; }

        //[Display(Name = "I enjoy West Coast IPAs")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesWestCoastIPA { get; set; }

        //[Display(Name = "I enjoy New Englad IPAs")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesNewEnglandIPA { get; set; }

        [Display(Name = "I enjoy Stouts")]
        [Range(typeof(int), "0", "10")]
        public int LikesStout { get; set; }

        [Display(Name = "I enjoy Porters")]
        [Range(typeof(int), "0", "10")]
        public int LikesPorter { get; set; }

        [Display(Name = "I enjoy Brown Ales")]
        [Range(typeof(int), "0", "10")]
        public int LikesBrownAle { get; set; }

        //[Display(Name = "I enjoy Ambers")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesAmber { get; set; }

        [Display(Name = "I enjoy Red Ales")]
        [Range(typeof(int), "0", "10")]
        public int LikesRedAle { get; set; }

        [Display(Name = "I enjoy Wheat Beers")]
        [Range(typeof(int), "0", "10")]
        public int LikesWheat { get; set; }

        //[Display(Name = "I enjoy Hefeweizens")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesHefeweizen { get; set; }

        //[Display(Name = "I enjoy wheat Belgian Witbiers")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesBelgianWit { get; set; }

        [Display(Name = "I enjoy Sours")]
        [Range(typeof(int), "0", "10")]
        public int LikesSourBeer { get; set; }

        //[Display(Name = "I enjoy Wild Ales")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesWildSour { get; set; }

        //[Display(Name = "I enjoy Berliner Weisses")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesBerlinerWeisseSour { get; set; }

        //[Display(Name = "I enjoy Goses")]
        //[Range(typeof(int), "0", "10")]
        //public int LikesGoseSour { get; set; }

        [Display(Name = "I enjoy Saisons")]
        [Range(typeof(int), "0", "10")]
        public int LikesSaison { get; set; }

        [Display(Name = "I enjoy Belgian beer styles")]
        [Range(typeof(int), "0", "10")]
        public int LikesBelgian { get; set; }

        //New
        [Display(Name = "I enjoy German beer styles")]
        [Range(typeof(int), "0", "10")]
        public int LikesGerman { get; set; }
    }
}
