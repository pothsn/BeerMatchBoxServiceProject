using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerMatchBoxService.Data;
using BeerMatchBoxService.Models;

namespace BeerMatchBoxService.Controllers
{
    public class BreweryDBBreweriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreweryDBBreweriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BreweryDBBreweries
        public async Task<IActionResult> Index()
        {
            return View(await _context.BreweryDBBrewery.ToListAsync());
        }

        // GET: BreweryDBBreweries/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryDBBrewery = await _context.BreweryDBBrewery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryDBBrewery == null)
            {
                return NotFound();
            }

            return View(breweryDBBrewery);
        }

        // GET: BreweryDBBreweries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BreweryDBBreweries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NameShortDisplay,Description,Website,Established,IsOrganic")] BreweryDBBrewery breweryDBBrewery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(breweryDBBrewery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(breweryDBBrewery);
        }

        // GET: BreweryDBBreweries/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryDBBrewery = await _context.BreweryDBBrewery.FindAsync(id);
            if (breweryDBBrewery == null)
            {
                return NotFound();
            }
            return View(breweryDBBrewery);
        }

        // POST: BreweryDBBreweries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NameShortDisplay,Description,Website,Established,IsOrganic")] BreweryDBBrewery breweryDBBrewery)
        {
            if (id != breweryDBBrewery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(breweryDBBrewery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryDBBreweryExists(breweryDBBrewery.Id))
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
            return View(breweryDBBrewery);
        }

        // GET: BreweryDBBreweries/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breweryDBBrewery = await _context.BreweryDBBrewery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (breweryDBBrewery == null)
            {
                return NotFound();
            }

            return View(breweryDBBrewery);
        }

        // POST: BreweryDBBreweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var breweryDBBrewery = await _context.BreweryDBBrewery.FindAsync(id);
            _context.BreweryDBBrewery.Remove(breweryDBBrewery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryDBBreweryExists(string id)
        {
            return _context.BreweryDBBrewery.Any(e => e.Id == id);
        }
    }
}
