using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BeerMatchBoxService.Models;

namespace BeerMatchBoxService.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BeerMatchBoxService.Models.User> User { get; set; }
        public DbSet<BeerMatchBoxService.Models.UserTaste> UserTaste { get; set; }
        public DbSet<BeerMatchBoxService.Models.BreweryDBBrewery> BreweryDBBrewery { get; set; }
        public DbSet<BeerMatchBoxService.Models.BreweryDBBeer> BreweryDBBeer { get; set; }
        public DbSet<BeerMatchBoxService.Models.UserBeer> UserBeer { get; set; }
    }
}
