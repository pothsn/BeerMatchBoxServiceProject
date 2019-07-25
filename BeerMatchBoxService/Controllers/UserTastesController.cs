﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeerMatchBoxService.Data;
using BeerMatchBoxService.Models;
using System.Security.Claims;

namespace BeerMatchBoxService.Controllers
{
    public class UserTastesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTastesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserTastes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserTaste.Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserTastes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTaste = await _context.UserTaste
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTaste == null)
            {
                return NotFound();
            }

            return View(userTaste);
        }

        // GET: UserTastes/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: UserTastes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,LikesBitter,LikesSour,LikesHoppy,LikesMalty,LikesSweet,LikesStrong,LikesSession,LikesOrganic,LikesPaleAle,LikesIPA,LikesWestCoastIPA,LikesNewEnglandIPA,LikesStout,LikesPorter,LikesBrownAle,LikesAmber,LikesRedAle,LikesWheat,LikesHefeweizen,LikesBelgianWit,LikesSourBeer,LikesWildSour,LikesBerlinerWeisseSour,LikesGoseSour,LikesSaison,LikesBelgianDoubleTrippel")] UserTaste userTaste)
        {
            if (ModelState.IsValid)
            {
                UserTaste newUserTaste = new UserTaste();

                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User loggedInUser = _context.User.Where(u => u.IdentityUserId == userId).SingleOrDefault();
                newUserTaste.UserId = loggedInUser.Id;
                _context.Add(newUserTaste);
                await _context.SaveChangesAsync();
                return RedirectToAction("Home","Users");
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userTaste.UserId);
            return View(userTaste);
        }

        // GET: UserTastes/Edit/5
        public async Task<IActionResult> Edit()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User loggedInUser = _context.User.Where(u => u.IdentityUserId == userId).SingleOrDefault();

            var userTaste = await _context.UserTaste.FindAsync(loggedInUser.Id);
            if (userTaste == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userTaste.UserId);
            return View(userTaste);
        }

        // POST: UserTastes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,LikesBitter,LikesSour,LikesHoppy,LikesMalty,LikesSweet,LikesStrong,LikesSession,LikesOrganic,LikesPaleAle,LikesIPA,LikesWestCoastIPA,LikesNewEnglandIPA,LikesStout,LikesPorter,LikesBrownAle,LikesAmber,LikesRedAle,LikesWheat,LikesHefeweizen,LikesBelgianWit,LikesSourBeer,LikesWildSour,LikesBerlinerWeisseSour,LikesGoseSour,LikesSaison,LikesBelgianDoubleTrippel")] UserTaste userTaste)
        {
            if (id != userTaste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTaste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTasteExists(userTaste.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Home", "Users");
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", userTaste.UserId);
            return View(userTaste);
        }

        // GET: UserTastes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTaste = await _context.UserTaste
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userTaste == null)
            {
                return NotFound();
            }

            return View(userTaste);
        }

        // POST: UserTastes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTaste = await _context.UserTaste.FindAsync(id);
            _context.UserTaste.Remove(userTaste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTasteExists(int id)
        {
            return _context.UserTaste.Any(e => e.Id == id);
        }
    }
}