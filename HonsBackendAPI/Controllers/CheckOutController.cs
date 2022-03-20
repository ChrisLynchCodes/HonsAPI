using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Interfaces;
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
        private readonly IOrderLineRepository _orderLinesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBasketRepository _basketRepository;

        public CheckOutController(BasketRepository basketRepository, OrderLineRepository orderLineRepository, CustomerRepository customersRepository, AddressRepository addressRepository, OrderRepository orderRepository, ProductRepository productRepository)
        {
            _addressRepository = addressRepository;
            _customersRepository = customersRepository;
            _ordersRepository = orderRepository;
            _productRepository = productRepository;
            _basketRepository = basketRepository;
            _orderLinesRepository = orderLineRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromForm] string basketId)
        {

            var domain = "https://uwssurvival.herokuapp.com/";

            //Get basket
            var basket = await _basketRepository.GetOneAsync(basketId);
            //Get Customer 
            var customer = await _customersRepository.GetOneAsync(basket.CustomerId);

            //Get list of products ids from basket
            var productIds = new List<string>();
            foreach (var line in basket.BasketProducts)
            {
                productIds.Add(line.ProductId);
            }
            //Get products matching ids
            var products = await _productRepository.GetManyAsync(productIds);
            
            //define order total
            decimal total = 0;
            //if productId == basketProduct.ProductId += total*quanttiy to total
            foreach(var product in products)
            {
                foreach(var line in basket.BasketProducts)
                {
                    if(product.Id == line.ProductId)
                    {
                        total += (product.Price * line.Quantity);
                    }
                }
            }









            //Create Order with cust id & total
            Models.Order orderModel = new()
            {
                CustomerId = basket.CustomerId,
                Total = total


            };
            await _ordersRepository.CreateAsync(orderModel);

            var order = await _ordersRepository.GetOrderForCustomerAsync(basket.CustomerId);

            //Create orderlines 
            foreach (var product in basket.BasketProducts)
            {
                var orderLine = new OrderLine();
                orderLine.ProductId = product.ProductId;
                orderLine.Quantity = product.Quantity;
                orderLine.OrderId = order.Id;

                _orderLinesRepository.CreateAsync(orderLine);

            }


            var items = new List<SessionLineItemOptions>();

            if (basket is not null)
            {
                foreach (var product in basket.BasketProducts)
                {


                    var stripeProduct = new SessionLineItemOptions
                    {
                        Price = product.StripePrice,
                        Quantity = product.Quantity,


                    };
                    items.Add(stripeProduct);
                }

            }

            //success url should call api endpoint to create orderlines and orders then redirect client to their orders page=================================
            //Or pass the checkout session id and take data from there
            //add customer and address to stripe checkout page from db

            var options = new Stripe.Checkout.SessionCreateOptions
            {
                CustomerEmail =customer.Email,
                LineItems = items,
                Mode = "payment",

                CancelUrl = domain + "checkout",
                SuccessUrl = domain + "ordersuccess",
                SubmitType = "pay",
                BillingAddressCollection = "auto",
                

              

            };
            var service = new Stripe.Checkout.SessionService();
            Stripe.Checkout.Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }






    }
}
