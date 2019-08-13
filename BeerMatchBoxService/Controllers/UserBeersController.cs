using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerMatchBoxService.Data;
using BeerMatchBoxService.Models;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace BeerMatchBoxService.Controllers
{
    public class UserBeersController : Controller
    {
        private readonly ApplicationDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public UserBeersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Add UserBeer from one of the index views
        public async Task<IActionResult> AddUserBeer(string breweryDBBeerId)
        {
            var beerURL = (APIKeys.BreweryDBAPIURL + "beer/" + breweryDBBeerId + "/?key=" + APIKeys.BreweryDBAPIKey);
            HttpResponseMessage response = await client.GetAsync(beerURL);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var beer = JsonConvert.DeserializeObject<JObject>(responseBody);
            
            UserBeer userBeer = new UserBeer();
            userBeer.BreweryDBBeerId = breweryDBBeerId;
            var name = beer["data"]["name"];
            userBeer.Name = name.ToObject<string>();
            if (beer["data"]["abv"] != null)
            {
                var abv = beer["data"]["abv"];
                userBeer.Abv = abv.ToObject<double>();
            }
            if (beer["data"]["ibu"] != null)
            {
                var ibu = beer["data"]["ibu"];
                userBeer.Ibu = ibu.ToObject<double>();
            }
            if (beer["data"]["style"] != null)
            {
                var styleId = beer["data"]["style"]["id"];
                if (styleId != null)
                {
                    userBeer.StyleId = styleId.ToObject<int>();
                }
                var style = beer["data"]["style"]["name"];
                if (style != null)
                {
                    userBeer.StyleName = style.ToObject<string>();
                }
            }
            var description = beer["data"]["description"];
            if (description != null)
            {
                userBeer.Description = description.ToObject<string>();
            }

            string findBeerBreweryURL = (APIKeys.BreweryDBAPIURL + "beer/" + breweryDBBeerId + "/breweries/?key=" + APIKeys.BreweryDBAPIKey);
            HttpResponseMessage thisResponse = await client.GetAsync(findBeerBreweryURL);
            thisResponse.EnsureSuccessStatusCode();
            string thisBreweryResponseBody = await thisResponse.Content.ReadAsStringAsync();
            var thisBreweryResult = JsonConvert.DeserializeObject<JObject>(thisBreweryResponseBody);

            var brewereyName = thisBreweryResult["data"][0]["name"];

            userBeer.BreweryName = brewereyName.ToObject<string>();

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User loggedInUser = _context.User.Where(u => u.IdentityUserId == userId).SingleOrDefault();
            userBeer.UserId = loggedInUser.Id;

            _context.Add(userBeer);
            await _context.SaveChangesAsync();
            return RedirectToAction("EditUserBeers");
        }

        public async Task<IActionResult> DeleteUserBeer(int userBeerId)
        {
            var userBeer = _context.UserBeer.Where(b => b.Id == userBeerId).FirstOrDefault();
            _context.UserBeer.Remove(userBeer);
            await _context.SaveChangesAsync();
            return RedirectToAction("EditUserBeers");
        }

        public async Task<IActionResult> EditUserBeers()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User loggedInUser = _context.User.Where(u => u.IdentityUserId == userId).SingleOrDefault();

            var userBeers = _context.UserBeer.Where(b => b.UserId == loggedInUser.Id).ToList();

            return View(userBeers);
        }
    }
}
