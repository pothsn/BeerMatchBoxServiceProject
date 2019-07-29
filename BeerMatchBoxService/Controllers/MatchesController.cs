﻿using System;
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








            //American - Style Barley Wine Ale x --points from: LikesStrong, LikesMiddling, LikesSweet, LikesFruity Strong Ale x

            //Specialty Stouts x-- points from: LikesStrong, LikesDark, LikesMalty, LikesChocolate, LikesStout, LikesCoffee

            //French and Belgian-Style Saison x --points from: LikesPale, LikesBelgian, LikesSaison, LikesAle

            //Belgian Style Pale Ale x-- points from: LikesPale, LikesPaleAle, LikesBelgian, LikesAle

            //Light American Wheat Ale or Lager with Yeast x --points from: LikesPale, LikesWheat

            //Imperial Red Ale x-- points from: LikesStrong, LikesMiddling, LikesRedAle, LikesMalty, (LikesHoppy, LikesAle?)

            //Fruit Wheat Ale or Lager with or without Yeast x-- points from: LikesFruity, LikesWheat

            return View(beers);
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
                beer.MatchId = id.ToObject<string>();
                var name = beers[i]["name"];
                beer.Name = name.ToObject<string>();
                if (beers[i]["style"] != null)
                {
                    var style = beers[i]["style"]["name"];
                    if (style != null)
                    {
                        beer.StyleName = style.ToObject<string>();
                    }
                }
                var abv = beers[i]["abv"];
                if (abv != null)
                {
                    beer.Abv = abv.ToObject<double>();
                }

                await GetBreweryInfo(beer);

                beerList.Add(beer);
            }
            return beerList;
        }

        public async Task<Match> GetBreweryInfo(Match beer)
        {
            string findBeerBreweryUrl = (APIKeys.BreweryDBAPIURL + "beer/" + beer.MatchId + "/breweries/?key=" + APIKeys.BreweryDBAPIKey);
            HttpResponseMessage thisResponse = await client.GetAsync(findBeerBreweryUrl);
            thisResponse.EnsureSuccessStatusCode();
            string thisBreweryResponseBody = await thisResponse.Content.ReadAsStringAsync();
            var thisBreweryResult = JsonConvert.DeserializeObject<JObject>(thisBreweryResponseBody);

            var breweryId = thisBreweryResult["data"][0]["id"];
            beer.BreweryDBBreweryId = breweryId.ToObject<string>();

            var brewereyName = thisBreweryResult["data"][0]["name"];
            beer.BreweryName = brewereyName.ToObject<string>();

            string findBeerBreweryLocationUrl = (APIKeys.BreweryDBAPIURL + "brewery/" + beer.BreweryDBBreweryId + "/locations/?key=" + APIKeys.BreweryDBAPIKey);
            HttpResponseMessage thisNewResponse = await client.GetAsync(findBeerBreweryLocationUrl);
            thisNewResponse.EnsureSuccessStatusCode();
            string thisNewBreweryResponseBody = await thisNewResponse.Content.ReadAsStringAsync();
            var thisNewBreweryResult = JsonConvert.DeserializeObject<JObject>(thisNewBreweryResponseBody);

            var breweryLat = thisNewBreweryResult["data"][0]["latitude"];
            if (breweryLat != null)
            {              
                beer.BeerBreweryLatitude = breweryLat.ToObject<decimal>();
            }
            var breweryLong = thisNewBreweryResult["data"][0]["longitude"];
            if (breweryLong["data"][0]["longitude"] != null)
            {
                beer.BeerBreweryLongitude = breweryLong.ToObject<decimal>();
            }
            var breweryAddress = thisNewBreweryResult["data"][0]["streetAddress"];
            if (breweryAddress != null)
            {
                beer.BeerBreweryAddress = breweryAddress.ToObject<string>();
            }
            var breweryCity = thisNewBreweryResult["data"][0]["locality"];
            if (breweryCity != null)
            {                
                beer.BeerBreweryCity = breweryCity.ToObject<string>();
            }
            var breweryState = thisNewBreweryResult["data"][0]["region"];
            if (breweryState != null)
            {
                beer.BeerBreweryState = breweryState.ToObject<string>();
            }
            return beer;
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
    }
}