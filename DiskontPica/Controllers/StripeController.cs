using DiskontPica.Helper;
using DiskontPica.Models;
using DiskontPica.Repository;
using Microsoft.AspNetCore.Authorization;
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
	[Route("api/stripe/")]
	[ApiController]
	public class StripeController : Controller
	{
		private readonly IDrinkStoreRepository _drinkStoreRepository;
		private readonly StripeSettings _settings;

		public string SessionId { get; set; }

		public StripeController(IDrinkStoreRepository drinkStoreRepository,IOptions<StripeSettings> settings)
		{
			_drinkStoreRepository = drinkStoreRepository ?? throw new ArgumentNullException(nameof(drinkStoreRepository));
			_settings = settings.Value;
		}

		[HttpPost]
		[Authorize(Policy = IdentityData.CustomerPolicy)]
		public ActionResult CreateCheckOutSession([FromBody]int orderId)
		{
			var existingOrder = _drinkStoreRepository.GetOrderById(orderId);

			if (existingOrder == null)
			{
				return NotFound("Order not found "+orderId);
			}

			var currancy = "rsd";
			var successUrl = "http://localhost:4200/customer-orders";
			

			StripeConfiguration.ApiKey = _settings.SecretKey;


			var options = new Stripe.Checkout.SessionCreateOptions
			{

				LineItems = new List<SessionLineItemOptions>
				{
					new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							Currency=currancy,
							UnitAmount=Convert.ToInt32(existingOrder.finalPrice*100),
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name=existingOrder.orderId.ToString()
							}
						},
						Quantity=1,
					}
				},
				Mode = "payment",
				SuccessUrl = successUrl,
			};

			var service=new Stripe.Checkout.SessionService();
			var session = service.Create(options);
			SessionId=session.Id;

			return Json(session.Url);

		}


		[HttpPost("webhook")]
		public async Task<IActionResult> Webhook()
		{


			Order order = _drinkStoreRepository.GetAllOrders().Last();
			_drinkStoreRepository.UpdateStatus(order.orderId);

			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			const string endpointSecret = "whsec_09442b785368600fc9baab3ac6f5c463013080d2acbc0c52e7d7672de74507ca";
			try
			{
				var stripeEvent = EventUtility.ParseEvent(json);
				var signatureHeader = Request.Headers["Stripe-Signature"];

				stripeEvent = EventUtility.ConstructEvent(json,
						signatureHeader, endpointSecret);

				if (stripeEvent.Type == Events.CheckoutSessionAsyncPaymentSucceeded)
				{
					var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
					
	
					
				}
				else if (stripeEvent.Type == Events.CheckoutSessionAsyncPaymentFailed)
				{
					var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
					

				}
				else
				{
					Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
				}
				return Ok();
			}
			catch (StripeException e)
			{
				Console.WriteLine("Error: {0}", e.Message);
				return BadRequest();
			}
			catch (Exception e)
			{
				return StatusCode(500);
			}
		}
	}


}

