using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
