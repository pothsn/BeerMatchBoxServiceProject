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

        // GET: UserBeers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserBeer.Include(u => u.BreweryDBBeer).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserBeers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBeer = await _context.UserBeer
                .Include(u => u.BreweryDBBeer)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBeer == null)
            {
                return NotFound();
            }

            return View(userBeer);
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
            return RedirectToAction("GetBeers", "BreweryDBBeers");
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

        // GET: UserBeers/Create
        public IActionResult Create()
        {
            ViewData["BreweryDBBeerId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: UserBeers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StyleName,StyleId,Abv,Ibu,UserId,BreweryDBBeerId")] UserBeer userBeer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userBeer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreweryDBBeerId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id", userBeer.BreweryDBBeerId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userBeer.UserId);
            return View(userBeer);
        }

        // GET: UserBeers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBeer = await _context.UserBeer.FindAsync(id);
            if (userBeer == null)
            {
                return NotFound();
            }
            ViewData["BreweryDBBeerId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id", userBeer.BreweryDBBeerId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userBeer.UserId);
            return View(userBeer);
        }

        // POST: UserBeers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StyleName,StyleId,Abv,Ibu,UserId,BreweryDBBeerId")] UserBeer userBeer)
        {
            if (id != userBeer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBeer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBeerExists(userBeer.Id))
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
            ViewData["BreweryDBBeerId"] = new SelectList(_context.BreweryDBBeer, "Id", "Id", userBeer.BreweryDBBeerId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userBeer.UserId);
            return View(userBeer);
        }

        // GET: UserBeers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userBeer = await _context.UserBeer
                .Include(u => u.BreweryDBBeer)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBeer == null)
            {
                return NotFound();
            }

            return View(userBeer);
        }

        // POST: UserBeers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userBeer = await _context.UserBeer.FindAsync(id);
            _context.UserBeer.Remove(userBeer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBeerExists(int id)
        {
            return _context.UserBeer.Any(e => e.Id == id);
        }
    }
}
