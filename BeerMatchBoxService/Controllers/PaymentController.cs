using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using BeerMatchBoxService.Models;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BeerMatchBoxService.Controllers
{
    public class PaymentController : Controller
    {
        private int amount = 4900;
        public IActionResult Index()
        {
            ViewBag.PaymentAmount = amount;
            ViewBag.StripePublishableAPIKey = APIKeys.StripePublishableAPIKey;
            return View();
        }

        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            Dictionary<string, string> Metadata = new Dictionary<string, string>();
            Metadata.Add("Product", "BeerBox");
            Metadata.Add("Quantity", "1");
            var options = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = "USD",
                Description = "Buying one beer box",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = Metadata
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            return View();
        }

        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public async Task<IActionResult> BuyBeerBox(string JSONModel)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<string>));
            var beerBox = (List<string>)ser.ReadObject(GenerateStreamFromString(JSONModel));

            var viewModel = new GetBoxOptionsViewModel();

            List<string> beerNames = new List<string>();

            foreach (string beerName in beerBox)
            {
                beerNames.Add(beerName);
            }

            viewModel.PreciseMatchBeerNames = beerNames;

            ViewBag.PaymentAmount = amount;
            ViewBag.StripePublishableAPIKey = APIKeys.StripePublishableAPIKey;


            return View("index", viewModel);
        }
    }
}
