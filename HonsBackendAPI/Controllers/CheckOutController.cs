using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {


        private readonly ICustomerRepository _customersRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _ordersRepository;
        private readonly IProductRepository _productRepository;

        public CheckOutController(CustomerRepository customersRepository, AddressRepository addressRepository, OrderRepository orderRepository, ProductRepository productRepository)
        {
            _addressRepository = addressRepository;
            _customersRepository = customersRepository;
            _ordersRepository = orderRepository;
            _productRepository = productRepository;
        }

        [HttpPost("Create")]
        public ActionResult Create([FromForm] string basketId)
        {

            //Get basket from db then rest of 'tables/documents' using basket contents

            var domain = "http://localhost:3000";
            var options = new Stripe.Checkout.SessionCreateOptions
            {

                LineItems = new List<SessionLineItemOptions>
                {

                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1KZQsQLs5lGv9x9Kr71VJ9Jf",
                    Quantity = 1,
                  },

                },
                Mode = "payment",
                SuccessUrl = domain + "?success=true",
                CancelUrl = domain + "?canceled=true",
                SubmitType = "pay",
                 BillingAddressCollection = "auto",
                ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                {
                    AllowedCountries = new List<string>
                  {

                    "GB",
                  },
                },
            };
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }








        //[HttpPost("PaymentIntent")]
        //public async Task<string> PaymentIntent([FromBody] CustomerBasket customerBasket)
        //{
        //    var productIds = new List<string>();
        //    foreach(var bP in customerBasket.BasketProducts)
        //    {
        //      productIds.Add(bP.ProductId);
        //    }
        //    var products = await _productRepository.GetManyAsync(productIds);

        //    decimal total = 0;

        //    foreach (var product in products)
        //    {
        //        total += product.Price;
        //        Console.WriteLine(product.Price);
        //    }


        //    var options = new PaymentIntentCreateOptions
        //    {
        //        Amount = (Convert.ToInt64(total) * 100),
        //        Currency = "gbp",
        //        AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
        //        {
        //            Enabled = true,
        //        },
        //    };
        //    var service = new PaymentIntentService();
        //    var intent = service.Create(options);

        //    return intent.ClientSecret;

        //}
    }
}
