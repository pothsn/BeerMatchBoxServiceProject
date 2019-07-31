using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using BeerMatchBoxService.Data;
using BeerMatchBoxService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeerMatchBoxService.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApplicationDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public MatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetMatches()
        {
            string IdentityId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User loggedInUser = _context.User.Where(u => u.IdentityUserId == IdentityId).SingleOrDefault();

            List<Match> beers = new List<Match>();

            if (CheckPilsener(loggedInUser))
            {
                var pilseners = await GetBeers("98");
                foreach (Match pilsner in pilseners)
                {
                    beers.Add(pilsner);
                }
            }
            if (CheckIPA(loggedInUser))
            {
                var IPAs = await GetBeers("30");
                foreach (Match ipa in IPAs)
                {
                    beers.Add(ipa);
                }
            }
            if (CheckSessionIPA(loggedInUser))
            {
                var sessionIPAs = await GetBeers("164");
                foreach (Match sesssionIpa in sessionIPAs)
                {
                    beers.Add(sesssionIpa);
                }
            }
            if (CheckPaleAle(loggedInUser))
            {
                var paleAles = await GetBeers("25");
                foreach (Match paleAle in paleAles)
                {
                    beers.Add(paleAle);
                }
            }
            if (CheckESB(loggedInUser))
            {
                var esbs = await GetBeers("5");
                foreach (Match esb in esbs)
                {
                    beers.Add(esb);
                }
            }
            if (CheckWheatWineAle(loggedInUser))
            {
                var wheatWineAles = await GetBeers("35");
                foreach (Match wheatWineALe in wheatWineAles)
                {
                    beers.Add(wheatWineALe);
                }
            }
            if (CheckStout(loggedInUser))
            {
                var stouts = await GetBeers("42");
                foreach (Match stout in stouts)
                {
                    beers.Add(stout);
                }
            }
            if (CheckSaison(loggedInUser))
            {
                var saisons = await GetBeers("72");
                foreach (Match saison in saisons)
                {
                    beers.Add(saison);
                }
            }
            if (CheckTripel(loggedInUser))
            {
                var tripels = await GetBeers("59");
                foreach (Match tripel in tripels)
                {
                    beers.Add(tripel);
                }
            }
            if (CheckBarrelAged(loggedInUser))
            {
                var barrelAgedStrongBeers = await GetBeers("135");
                foreach (Match basb in barrelAgedStrongBeers)
                {
                    beers.Add(basb);
                }
            }
            if (CheckLager(loggedInUser))
            {
                var lagers = await GetBeers("93");
                foreach (Match lager in lagers)
                {
                    beers.Add(lager);
                }
            }
            if (CheckDipa(loggedInUser))
            {
                var dipas = await GetBeers("173");
                foreach (Match dipa in dipas)
                {
                    beers.Add(dipa);
                }
            }
            if (CheckFlanders(loggedInUser))
            {
                var bsfobs = await GetBeers("57");
                foreach (Match bsfob in bsfobs)
                {
                    beers.Add(bsfob);
                }
            }
            if (CheckImperialPorter(loggedInUser))
            {
                var imperialPorters = await GetBeers("158");
                foreach (Match imperialPorter in imperialPorters)
                {
                    beers.Add(imperialPorter);
                }
            }
            if (CheckHeller(loggedInUser))
            {
                var hellers = await GetBeers("89");
                foreach (Match heller in hellers)
                {
                    beers.Add(heller);
                }
            }
            if (CheckImperialStout(loggedInUser))
            {
                var imperialStouts = await GetBeers("16");
                foreach (Match imperialStout in imperialStouts)
                {
                    beers.Add(imperialStout);
                }
            }
            if (CheckBarleyWine(loggedInUser))
            {
                var barleyWines = await GetBeers("34");
                foreach (Match barleyWine in barleyWines)
                {
                    beers.Add(barleyWine);
                }
            }
            if (CheckStrongAle(loggedInUser))
            {
                var strongAles = await GetBeers("14");
                foreach (Match strongAle in strongAles)
                {
                    beers.Add(strongAle);
                }
            }
            if (CheckSpecialtyStout(loggedInUser))
            {
                var specialtyStouts = await GetBeers("44");
                foreach (Match specialtyStout in specialtyStouts)
                {
                    beers.Add(specialtyStout);
                }
            }
            if (CheckSpecialBitter(loggedInUser))
            {
                var specialBitters = await GetBeers("4");
                foreach (Match specialBitter in specialBitters)
                {
                    beers.Add(specialBitter);
                }
            }
            if (CheckBelgianPale(loggedInUser))
            {
                var belgianPales = await GetBeers("62");
                foreach (Match belgianPale in belgianPales)
                {
                    beers.Add(belgianPale);
                }
            }
            if (CheckLightAmericanWheatAleOrLagerWithYeast(loggedInUser))
            {
                var awys = await GetBeers("112");
                foreach (Match awy in awys)
                {
                    beers.Add(awy);
                }
            }
            if (CheckImperialRedAle(loggedInUser))
            {
                var iras = await GetBeers("33");
                foreach (Match ira in iras)
                {
                    beers.Add(ira);
                }
            }
            if (CheckFruitWheatAleOrLagerWithoutYeast(loggedInUser))
            {
                var fwawowys = await GetBeers("114");
                foreach (Match fwawowy in fwawowys)
                {
                    beers.Add(fwawowy);
                }
            }

            var userBeersAverageAbv = GetUserBeersAverageAbv(loggedInUser);

            var filteredBeers = await FilterBeers(beers, userBeersAverageAbv);

            foreach (Match beer in filteredBeers)
            {
                await GetBreweryInfo(beer);
            }

            var doughnutSections = await GenerateDoughnut(filteredBeers);


            var viewModel = new GetMatchesViewModel();
            viewModel.Matches = filteredBeers;
            viewModel.UserTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            viewModel.DoughnutSections = doughnutSections;


            return View(viewModel);
        }

        public bool CheckPilsener(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesLager > 6 && userTaste.LikesHoppy > 2 && userTaste.LikesBitter > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIPA(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesHoppy > 6 && userTaste.LikesBitter > 6 && userTaste.LikesIPA > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckSessionIPA(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesSession > 6 && userTaste.LikesHoppy > 6 && userTaste.LikesBitter > 6 && userTaste.LikesIPA > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPaleAle(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesHoppy > 6 && userTaste.LikesBitter > 6 && userTaste.LikesPale > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckESB(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesBitter > 6 && userTaste.LikesMalty > 6 && userTaste.LikesMiddling > 6 && userTaste.LikesAle > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckWheatWineAle(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesFruity > 6 && userTaste.LikesWheat > 6 && userTaste.LikesStrong > 6 && userTaste.LikesAle > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckStout(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesDark > 6 && userTaste.LikesMalty > 6 && userTaste.LikesStout > 6 && userTaste.LikesCoffee > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckSaison(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesBelgian > 6 && userTaste.LikesSaison > 6 && userTaste.LikesAle > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckTripel(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesStrong > 6 && userTaste.LikesBelgian > 6 && userTaste.LikesAle > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckBarrelAged(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesMiddling > 6 && userTaste.LikesDark > 6 && userTaste.LikesStrong > 6 && userTaste.LikesBarrelAged > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckLager(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesLager > 6 && userTaste.LikesHoppy > 3 && userTaste.LikesBitter > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckDipa(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesStrong > 6 && userTaste.LikesIPA > 6 && userTaste.LikesHoppy > 6 && userTaste.LikesBitter > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckFlanders(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesSour > 6 && userTaste.LikesSourBeer > 6 && userTaste.LikesMiddling > 6 && userTaste.LikesBelgian > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckImperialPorter(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesStrong > 6 && userTaste.LikesPorter > 6 && userTaste.LikesDark > 6 && userTaste.LikesChocolate > 6 && userTaste.LikesMalty > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckHeller(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesGerman > 6 && userTaste.LikesPale > 6 && userTaste.LikesStrong > 6 && userTaste.LikesHoppy > 3 && userTaste.LikesBitter > 3 && userTaste.LikesMalty > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckImperialStout(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesStrong > 6 && userTaste.LikesDark > 6 && userTaste.LikesMalty > 6 && userTaste.LikesChocolate > 6 && userTaste.LikesStout > 6 && userTaste.LikesCoffee > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckBarleyWine(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesStrong > 6 && userTaste.LikesMiddling > 6 && userTaste.LikesSweet > 6 && userTaste.LikesFruity > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckStrongAle(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesStrong > 6 && userTaste.LikesMiddling > 6 && userTaste.LikesMalty > 6 && userTaste.LikesAle > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckSpecialtyStout(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesStrong > 6 && userTaste.LikesDark > 6 && userTaste.LikesMalty > 6 && userTaste.LikesChocolate > 6 && userTaste.LikesStout > 6 && userTaste.LikesCoffee > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckSpecialBitter(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesMiddling > 6 && userTaste.LikesMalty > 5 && userTaste.LikesHoppy > 5 && userTaste.LikesBitter > 6 && userTaste.LikesSweet > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckBelgianPale(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesPaleAle > 5 && userTaste.LikesBelgian > 5 && userTaste.LikesAle > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckLightAmericanWheatAleOrLagerWithYeast(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesPale > 6 && userTaste.LikesWheat > 5 && userTaste.LikesLager > 5 && userTaste.LikesAle > 6 && userTaste.LikesHoppy > 3 && userTaste.LikesBitter > 3 && userTaste.LikesFruity > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckImperialRedAle(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesStrong > 6 && userTaste.LikesMiddling > 5 && userTaste.LikesRedAle > 5 && userTaste.LikesMalty > 6 && userTaste.LikesHoppy > 6 && userTaste.LikesAle > 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckFruitWheatAleOrLagerWithoutYeast(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault();
            if (userTaste.LikesFruity > 7 && userTaste.LikesWheat > 6 && userTaste.LikesLager > 5 && userTaste.LikesLager > 5 && userTaste.LikesHoppy > 3 && userTaste.LikesBitter > 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Match>> GetBeers(string styleId)
        {
            var url = (APIKeys.BreweryDBAPIURL + "beers/?styleId=" + styleId + "&key=" + APIKeys.BreweryDBAPIKey);
            List<Match> beerList = new List<Match>();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<JObject>(responseBody);
            var beers = result["data"].ToList();
            for (var i = 0; i < beers.Count; i++)
            {
                Match beer = new Match();
                var id = beers[i]["id"];
                beer.BreweryDBBeerId = id.ToObject<string>();
                var name = beers[i]["name"];
                beer.Name = name.ToObject<string>();
                var abv = beers[i]["abv"];

                if (abv != null)
                {
                    beer.Abv = abv.ToObject<double>();
                }
                var ibu = beers[i]["ibu"];
                if (ibu != null)
                {
                    beer.Ibu = ibu.ToObject<double>();
                }
                beer.StyleId = styleId;
                var style = beers[i]["style"]["name"];
                if (style != null)
                {
                    beer.StyleName = style.ToObject<string>();
                }
                var description = beers[i]["description"];
                if (description != null)
                {
                    beer.Description = description.ToObject<string>();
                }                         
                beerList.Add(beer);
            }
            return beerList;
        }

        public double? GetUserBeersAverageAbv(User loggedInUser)
        {
            var userBeers = _context.UserBeer.Where(b => b.UserId == loggedInUser.Id).ToList();
            Double? userBeerAbvSum = new double();
            for (var i = 0; i < userBeers.Count; i++)
            {
                userBeerAbvSum += userBeers[i].Abv;
            }
            var userBeerAbvAverage = userBeerAbvSum / userBeers.Count;

            return userBeerAbvAverage;
        }

        public async Task<List<Match>> FilterBeers(List<Match> beers, double? userBeersAverageAbv)
        {
            //order list and cut off far ends before the loop?
            var filteredBeers = new List<Match>();
            var incrementor = .1;
            while (filteredBeers.Count < 5)
            {
                foreach (Match beer in beers)
                {
                    if (beer.Abv < (userBeersAverageAbv + incrementor) && beer.Abv > (userBeersAverageAbv - incrementor) && filteredBeers.Count < 5)
                    {
                        filteredBeers.Add(beer);
                    }
                }
                incrementor += .1;
            }
            return filteredBeers;
        }

        public async Task<List<DoughnutSection>> GenerateDoughnut(List<Match> beers)
        {
            var doughnutSections = new List<DoughnutSection>();
            
            foreach (Match beer in beers)
            {
                int numberOfSlices = doughnutSections.Count(i => i.StyleId == beer.StyleId);
                switch (numberOfSlices)
                {
                    case 0:
                        DoughnutSection newDoughnutSection = new DoughnutSection();
                        newDoughnutSection.StyleId = beer.StyleId;
                        newDoughnutSection.StyleName = beer.StyleName;
                        newDoughnutSection.Percentage += 20;
                        doughnutSections.Add(newDoughnutSection);
                        break;
                    default:
                        var thisDougnutSection = doughnutSections.Find(s => s.StyleId == beer.StyleId);
                        thisDougnutSection.Percentage += 20;
                        break;
                }
            } 
            while (doughnutSections.Count < 5)
            {
                DoughnutSection newDoughnutSection = new DoughnutSection();
                newDoughnutSection.StyleName = "N/A";
                newDoughnutSection.Percentage = 0;
                doughnutSections.Add(newDoughnutSection);
            }
            return doughnutSections;
        }

        public async Task<Match> GetBreweryInfo(Match beer)
        {
            string findBeerBreweryUrl = (APIKeys.BreweryDBAPIURL + "beer/" + beer.BreweryDBBeerId + "/breweries/?key=" + APIKeys.BreweryDBAPIKey);
            HttpResponseMessage thisResponse = await client.GetAsync(findBeerBreweryUrl);
            thisResponse.EnsureSuccessStatusCode();
            string thisBreweryResponseBody = await thisResponse.Content.ReadAsStringAsync();
            var thisBreweryResult = JsonConvert.DeserializeObject<JObject>(thisBreweryResponseBody);

            var breweryId = thisBreweryResult["data"][0]["id"];
            beer.BreweryDBBreweryId = breweryId.ToObject<string>();

            var brewereyName = thisBreweryResult["data"][0]["name"];
            beer.BreweryName = brewereyName.ToObject<string>();

            //string findBeerBreweryLocationUrl = (APIKeys.BreweryDBAPIURL + "brewery/" + beer.BreweryDBBreweryId + "/locations/?key=" + APIKeys.BreweryDBAPIKey);
            //HttpResponseMessage thisNewResponse = await client.GetAsync(findBeerBreweryLocationUrl);
            //thisNewResponse.EnsureSuccessStatusCode();
            //string thisNewBreweryResponseBody = await thisNewResponse.Content.ReadAsStringAsync();
            //var thisNewBreweryResult = JsonConvert.DeserializeObject<JObject>(thisNewBreweryResponseBody);

            //var breweryLat = thisNewBreweryResult["data"][0]["latitude"];
            //if (breweryLat != null)
            //{
            //    beer.BeerBreweryLatitude = breweryLat.ToObject<decimal>();
            //}
            //var breweryLong = thisNewBreweryResult["data"][0]["longitude"];
            //if (breweryLong != null)
            //{
            //    beer.BeerBreweryLongitude = breweryLong.ToObject<decimal>();
            //}
            //var breweryAddress = thisNewBreweryResult["data"][0]["streetAddress"];
            //if (breweryAddress != null)
            //{
            //    beer.BeerBreweryAddress = breweryAddress.ToObject<string>();
            //}
            //var breweryCity = thisNewBreweryResult["data"][0]["locality"];
            //if (breweryCity != null)
            //{
            //    beer.BeerBreweryCity = breweryCity.ToObject<string>();
            //}
            //var breweryState = thisNewBreweryResult["data"][0]["region"];
            //if (breweryState != null)
            //{
            //    beer.BeerBreweryState = breweryState.ToObject<string>();
            //}
            return beer;
        }

        public async Task<IActionResult> GetMatchBreweryInfo(string BreweryDBBreweryId)
        {
            ViewBag.GoogleMapsAPIKey = APIKeys.GoogleMapsAPIKey;
            string findBeerBreweryLocationUrl = (APIKeys.BreweryDBAPIURL + "brewery/" + BreweryDBBreweryId + "/locations/?key=" + APIKeys.BreweryDBAPIKey);
            HttpResponseMessage thisNewResponse = await client.GetAsync(findBeerBreweryLocationUrl);
            thisNewResponse.EnsureSuccessStatusCode();
            string thisNewBreweryResponseBody = await thisNewResponse.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<JObject>(thisNewBreweryResponseBody);

            BreweryDBBrewery brewery = new BreweryDBBrewery();


            brewery.BreweryDBBreweryId = BreweryDBBreweryId;

            var name = data["data"][0]["name"];
            brewery.Name = name.ToObject<string>();




            var breweryLat = data["data"][0]["latitude"];
            if (breweryLat != null)
            {
                brewery.Latitude = breweryLat.ToObject<decimal>();
            }
            var breweryLong = data["data"][0]["longitude"];
            if (breweryLong != null)
            {
                brewery.Longitude = breweryLong.ToObject<decimal>();
            }
            var breweryAddress = data["data"][0]["streetAddress"];
            if (breweryAddress != null)
            {
                brewery.Address = breweryAddress.ToObject<string>();
            }
            var breweryCity = data["data"][0]["locality"];
            if (breweryCity != null)
            {
                brewery.City = breweryCity.ToObject<string>();
            }
            var breweryState = data["data"][0]["region"];
            if (breweryState != null)
            {
                brewery.State = breweryState.ToObject<string>();
            }


            return View(brewery);
        }


















    }
}