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

namespace BeerMatchBoxService.Controllers
{
    public class BreweryDBBreweriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        static readonly HttpClient client = new HttpClient();

        public BreweryDBBreweriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetBreweries()
        {
            string breweryDBUrl = (APIKeys.BreweryDBAPIURL + "breweries/?key=" + APIKeys.BreweryDBAPIKey);
            try
            {
                HttpResponseMessage response = await client.GetAsync(breweryDBUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<JObject>(responseBody);
                var breweries = result["data"].ToList();

                List<BreweryDBBrewery> breweryDBBreweries = new List<BreweryDBBrewery>();

                for (var i = 0; i < breweries.Count; i++)
                {
                    BreweryDBBrewery brewery = new BreweryDBBrewery();
                    var id = breweries[i]["id"];
                    brewery.BreweryDBBreweryId = id.ToObject<string>();
                    var name = breweries[i]["name"];
                    brewery.Name = name.ToObject<string>();

                    breweryDBBreweries.Add(brewery);
                }
                return View(breweryDBBreweries);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return View();
        }
    }
}
