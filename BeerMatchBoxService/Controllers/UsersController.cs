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
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BeerMatchBoxService.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        //HOME Action
        public IActionResult Home()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User loggedInUser = _context.User.Where(u => u.IdentityUserId == userId).SingleOrDefault();
            return View(loggedInUser);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.User.Include(u => u.IdentityUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Address,City,State,Zipcode,Latitude,Longitude,IdentityUserId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.IdentityUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _context.Add(user);
                await Geocode(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "UserTastes");
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", user.IdentityUserId);
            return View();
        }

        static async Task Geocode(User user)
        {
            string breweryURL = ("https://maps.googleapis.com/maps/api/geocode/json?address=" + user.Address + user.City + user.State + APIKeys.GoogleGeocodingAPI);
            try
            {
                HttpResponseMessage response = await client.GetAsync(breweryURL);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JObject>(responseBody);
                var latitude = result["results"][0]["geometry"]["location"]["lat"];
                user.Latitude = latitude.ToObject<double>();
                var longitude = result["results"][0]["geometry"]["location"]["lng"];
                user.Longitude = longitude.ToObject<double>();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }



        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", user.IdentityUserId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,City,State,Zipcode,Latitude,Longitude,IdentityUserId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", "Id", user.IdentityUserId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .Include(u => u.IdentityUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
