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
