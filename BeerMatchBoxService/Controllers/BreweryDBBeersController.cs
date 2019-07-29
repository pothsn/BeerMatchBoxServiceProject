using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerMatchBoxService.Data;
using BeerMatchBoxService.Models;
using BreweryDbStandard;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

namespace BeerMatchBoxService.Controllers
{
    public class BreweryDBBeersController : Controller
    {
        private readonly ApplicationDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public BreweryDBBeersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetBeers()
        {
            //Establish all beer query URL
            string breweryDBUrl = (APIKeys.BreweryDBAPIURL + "beers/?key=" + APIKeys.BreweryDBAPIKey);
            try
            {
                HttpResponseMessage response = await client.GetAsync(breweryDBUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var thisResult = JsonConvert.DeserializeObject<JObject>(responseBody);
                
                var beers = thisResult["data"].ToList();

                List<BreweryDBBeer> breweryDBBeers = new List<BreweryDBBeer>();

                //LATER either break out below into different function or create new view/action and only query for brewery if they click it
                for (var i = 0; i < beers.Count; i++)
                {
                    BreweryDBBeer breweryDBBeer = new BreweryDBBeer();
                    var id = beers[i]["id"];
                    breweryDBBeer.BreweryDBBeerId = id.ToObject<string>();
                    var name = beers[i]["name"];
                    breweryDBBeer.Name = name.ToObject<string>();
                    if (beers[i]["style"] != null)
                    {
                        var styleId = beers[i]["style"]["id"];
                        if (styleId != null)
                        {
                            breweryDBBeer.StyleId = styleId.ToObject<int>();
                        }
                        var style = beers[i]["style"]["name"];
                        if (style != null)
                        {
                            breweryDBBeer.StyleName = style.ToObject<string>();
                        }
                    }                  
                    var abv = beers[i]["abv"];
                    if (abv != null)
                    {
                        breweryDBBeer.Abv = abv.ToObject<double>();
                    }
                    var ibu = beers[i]["ibu"];
                    if (ibu != null)
                    {
                        breweryDBBeer.Ibu = ibu.ToObject<double>();
                    }
                    var description = beers[i]["description"];
                    if (description != null)
                    {
                        breweryDBBeer.Description = description.ToObject<string>();
                    }

                    string findBeerBreweryURL = (APIKeys.BreweryDBAPIURL + "beer/" + breweryDBBeer.BreweryDBBeerId + "/breweries/?key=" + APIKeys.BreweryDBAPIKey);
                    HttpResponseMessage thisResponse = await client.GetAsync(findBeerBreweryURL);
                    thisResponse.EnsureSuccessStatusCode();
                    string thisBreweryResponseBody = await thisResponse.Content.ReadAsStringAsync();
                    var thisBreweryResult = JsonConvert.DeserializeObject<JObject>(thisBreweryResponseBody);
                   
                    var brewereyName = thisBreweryResult["data"][0]["name"];
                    
                    breweryDBBeer.BreweryName = brewereyName.ToObject<string>();

                    breweryDBBeers.Add(breweryDBBeer);                    
                }
                return View(breweryDBBeers);              
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return View();
        }

        public async Task<IActionResult> GetBreweryBeers(string breweryDBBreweryId)
        {
            var breweryBeersUrl = (APIKeys.BreweryDBAPIURL + "brewery/" + breweryDBBreweryId + "/beers/?key=" + APIKeys.BreweryDBAPIKey);
            try
            {
                HttpResponseMessage response = await client.GetAsync(breweryBeersUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JObject>(responseBody);
                var beers = result["data"].ToList();

                List<BreweryDBBeer> breweryDBBeers = new List<BreweryDBBeer>();

                for (var i = 0; i < beers.Count; i++)
                {
                    BreweryDBBeer breweryDBBeer = new BreweryDBBeer();
                    var id = beers[i]["id"];
                    breweryDBBeer.BreweryDBBeerId = id.ToObject<string>();
                    var name = beers[i]["name"];
                    breweryDBBeer.Name = name.ToObject<string>();
                    if (beers[i]["style"] != null)
                    {
                        var style = beers[i]["style"]["name"];
                        if (style != null)
                        {
                            breweryDBBeer.StyleName = style.ToObject<string>();
                        }
                    }
                    var abv = beers[i]["abv"];
                    if (abv != null)
                    {
                        breweryDBBeer.Abv = abv.ToObject<double>();
                    }

                    string findBeerBreweryURL = (APIKeys.BreweryDBAPIURL + "beer/" + breweryDBBeer.BreweryDBBeerId + "/breweries/?key=" + APIKeys.BreweryDBAPIKey);
                    HttpResponseMessage thisResponse = await client.GetAsync(findBeerBreweryURL);
                    thisResponse.EnsureSuccessStatusCode();
                    string thisBreweryResponseBody = await thisResponse.Content.ReadAsStringAsync();
                    var thisBreweryResult = JsonConvert.DeserializeObject<JObject>(thisBreweryResponseBody);

                    var brewereyName = thisBreweryResult["data"][0]["name"];

                    breweryDBBeer.BreweryName = brewereyName.ToObject<string>();

                    breweryDBBeers.Add(breweryDBBeer);
                }
                return View(breweryDBBeers);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return View();
        }

        public async Task<IActionResult> GetMatches ()
        {
            string IdentityId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User loggedInUser = _context.User.Where(u => u.IdentityUserId == IdentityId).SingleOrDefault();
            List<BreweryDBBeer> beers = new List<BreweryDBBeer>();

            //American - Style Pilsener xxxx --points from: LikesPale, LikesLager, (medium LikesHoppy, LikesBitter)
            if (CheckPilsener(loggedInUser))
            {
                //pull pilseners from DB
                var pilseners = await GetPilseners();
            }

            //American - Style India Pale Ale xxxx --points from: LikesPale, LikesHoppy, LikesBitter, LikesIPA

            //Session India Pale Ale xx --points from: LikesSesson, LikesHoppy, LikesBitter, LikesIPA

            //American - Style Pale Ale xxxxx-- points from: LikesPale, LikesHoppy, LikesBitter, LikesPaleAle

            //Extra Special Bitter xx-- points from: LikesBitter, LikesMalty, LikesMiddling, LikesAle

            //American - Style Wheat Wine Ale x --points from: LikesFruity, LikesWheat, LikesStrong, LikesAle

            //American Style Stout x-- points from: LikesDark, LikesMalty, LikesStout, LikesCoffee

            //French & Belgian - Style Saison x --points from: LikesPale, LikesBelgian, LikesSaison, LikesAle

            //Belgian - Style Tripel x --points from: LikesPale, LikesStrong, LikesBelgian, LikesAle

            //Wood - and Barrel - Aged Strong Beer x-- points from: LikesMiddling, LikesDark, LikesStrong, LikesBarrelAged

            //American - Style Lager x --points from: LikesPale, LikesLager, LikesHoppy, LikesBitter

            //Imperial or Double India Pale Ale xxxxxxxx --points from: LikesStrong, LikesIPA, LikesHoppy, LikesBitter

            //Belgian - Style Flanders Old Bruin or Oud Red Ales x --points from: LikesSour, LikesSourBeer, LikesMiddling, LikesBelgian

            //American - Style Imperial Porter x-- points from: LikesStrong, LikesPorter, LikesDark, LikesChocolate, LikesMalty

            //German - Style Heller Bock/ Maibock x-- points from: LikesGerman, LikesPale, LikesStrong, (low LikesHoppy / bitter / malt ?)

            //American - Style Imperial Stout x-- points from: LikesStrong, LikesDark, LikesMalty, LikesChocolate, LikesStout, LikesCoffee

            //American - Style Barley Wine Ale x --points from: LikesStrong, LikesMiddling, LikesSweet, LikesFruity Strong Ale x

            //Specialty Stouts x-- points from: LikesStrong, LikesDark, LikesMalty, LikesChocolate, LikesStout, LikesCoffee

            //French and Belgian-Style Saison x --points from: LikesPale, LikesBelgian, LikesSaison, LikesAle

            //Belgian Style Pale Ale x-- points from: LikesPale, LikesPaleAle, LikesBelgian, LikesAle

            //Light American Wheat Ale or Lager with Yeast x --points from: LikesPale, LikesWheat

            //Imperial Red Ale x-- points from: LikesStrong, LikesMiddling, LikesRedAle, LikesMalty, (LikesHoppy, LikesAle?)

            //Fruit Wheat Ale or Lager with or without Yeast x-- points from: LikesFruity, LikesWheat







            return RedirectToAction("Home", "Users");
        }


        public bool CheckPilsener(User loggedInUser)
        {
            var userTaste = _context.UserTaste.Where(u => u.UserId == loggedInUser.Id).FirstOrDefault(); ;
            if (userTaste.LikesPale > 6 && userTaste.LikesLager > 6 && userTaste.LikesHoppy > 2 && userTaste.LikesBitter > 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<BreweryDBBeer>> GetPilseners()
        {
            var pilsenerUrl = (APIKeys.BreweryDBAPIURL + "beers/?styleId=98&key=" + APIKeys.BreweryDBAPIKey);
            List<BreweryDBBeer> pilseners = new List<BreweryDBBeer>();
            HttpResponseMessage response = await client.GetAsync(pilsenerUrl);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<JObject>(responseBody);
            var beers = result["data"].ToList();
            for (var i = 0; i < beers.Count; i++)
            {
                BreweryDBBeer breweryDBBeer = new BreweryDBBeer();
                var id = beers[i]["id"];
                breweryDBBeer.BreweryDBBeerId = id.ToObject<string>();
                var name = beers[i]["name"];
                breweryDBBeer.Name = name.ToObject<string>();
                if (beers[i]["style"] != null)
                {
                    var style = beers[i]["style"]["name"];
                    if (style != null)
                    {
                        breweryDBBeer.StyleName = style.ToObject<string>();
                    }
                }
                var abv = beers[i]["abv"];
                if (abv != null)
                {
                    breweryDBBeer.Abv = abv.ToObject<double>();
                }

                string findBeerBreweryURL = (APIKeys.BreweryDBAPIURL + "beer/" + breweryDBBeer.BreweryDBBeerId + "/breweries/?key=" + APIKeys.BreweryDBAPIKey);
                HttpResponseMessage thisResponse = await client.GetAsync(findBeerBreweryURL);
                thisResponse.EnsureSuccessStatusCode();
                string thisBreweryResponseBody = await thisResponse.Content.ReadAsStringAsync();
                var thisBreweryResult = JsonConvert.DeserializeObject<JObject>(thisBreweryResponseBody);

                var brewereyName = thisBreweryResult["data"][0]["name"];

                breweryDBBeer.BreweryName = brewereyName.ToObject<string>();

                pilseners.Add(breweryDBBeer);
            }
            return pilseners;
        }



















       
        // GET: BreweryDBBeers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BreweryDBBeer;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: BreweryDBBeers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryDBBeer = await _context.BreweryDBBeer
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryDBBeer == null)
            {
                return NotFound();
            }

            return View(breweryDBBeer);
        }

        // GET: BreweryDBBeers/Create
        public IActionResult Create()
        {
            ViewData["BreweryDBBreweryId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id");
            return View();
        }

        // POST: BreweryDBBeers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abv,GlasswareId,StyleId,IsOrganic,IsRetired,BreweryDBBreweryId")] BreweryDBBeer breweryDBBeer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(breweryDBBeer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreweryDBBreweryId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id");
            return View(breweryDBBeer);
        }

        // GET: BreweryDBBeers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryDBBeer = await _context.BreweryDBBeer.FindAsync(id);
            if (breweryDBBeer == null)
            {
                return NotFound();
            }
            ViewData["BreweryDBBreweryId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id");
            return View(breweryDBBeer);
        }

        // POST: BreweryDBBeers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Abv,GlasswareId,StyleId,IsOrganic,IsRetired,BreweryDBBreweryId")] BreweryDBBeer breweryDBBeer)
        {
            if (id != breweryDBBeer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breweryDBBeer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryDBBeerExists(breweryDBBeer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreweryDBBreweryId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id");
            return View(breweryDBBeer);
        }

        // GET: BreweryDBBeers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryDBBeer = await _context.BreweryDBBeer
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryDBBeer == null)
            {
                return NotFound();
            }

            return View(breweryDBBeer);
        }

        // POST: BreweryDBBeers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var breweryDBBeer = await _context.BreweryDBBeer.FindAsync(id);
            _context.BreweryDBBeer.Remove(breweryDBBeer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryDBBeerExists(string id)
        {
            return _context.BreweryDBBeer.Any(e => e.Id == id);
        }
    }
}
