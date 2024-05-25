using DiskontPica.Helper;
using DiskontPica.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;

namespace DiskontPica.Controllers
{
	[EnableCors("AllowCors")]
	[Route("api/stripe")]
	public class StripeController : Controller
	{
		private readonly StripeSettings _settings;

		public string SessionId { get; set; }

		public StripeController(IOptions<StripeSettings> settings)
		{
			_settings = settings.Value;
		}

		[HttpPost]
		public IActionResult CreateCheckOutSession([FromBody] Order order)
		{
			var currancy = "rsd";
			var successUrl = "http://localhost:4200/drinkStore/product";

			StripeConfiguration.ApiKey = _settings.SecretKey;


			var options = new Stripe.Checkout.SessionCreateOptions
            {
				PaymentMethodTypes = new List<string> { "card" },

				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							Currency=currancy,
							UnitAmount =Convert.ToInt32(order.finalPrice),
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = order.orderId.ToString()
							}
						},
						Quantity=1
					}
				},
				Mode = "payment",
				SuccessUrl = successUrl,
			};

			var service=new Stripe.Checkout.SessionService();
			var session = service.Create(options);
			SessionId=session.Id;

			return Redirect(session.Url);

		}
	}
}
