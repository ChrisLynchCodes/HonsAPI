using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Interfaces;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {

        private readonly IBasketRepository _basketRepository;

        public BasketsController(BasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }


        //Get baskets 
        // GET api/<BasketsController>
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> Get()
        {
            var basketModels = await _basketRepository.GetAllAsync();

            if (basketModels is null)
            {
                return NotFound();
            }

            return Ok(basketModels);
        }


        // GET api/<BasketsController>/5
        [HttpGet("{basketId:length(24)}")]
        public async Task<ActionResult<CustomerBasket>> Get(string basketId)
        {
            var basketModel = await _basketRepository.GetOneAsync(basketId);

            if (basketModel is null)
            {
                return NotFound();
            }

            return Ok(basketModel);
        }
        // GET api/<BasketsController>/5
        [HttpGet]
        [Route("[action]/{customerId}")]
        public async Task<ActionResult<CustomerBasket>> GetByCustomerId(string customerId)
        {
            var basketModel = await _basketRepository.GetByCustomerIdAsync(customerId);

            if (basketModel is null)
            {
                return NotFound();
            }

            return Ok(basketModel);
        }


        //Create a basket 
        // POST api/<BasketsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerBasket customerBasket)
        {
            if (ModelState.IsValid)
            {
                 await _basketRepository.CreateAsync(customerBasket);
                 return CreatedAtAction(nameof(Get), new { basketId = customerBasket.Id }, customerBasket);

            }

            return NotFound();

        }


        // PUT api/<BasketsController>/5
        [HttpPut("{basketId:length(24)}")]
        public async Task<IActionResult> Update(string basketId, [FromBody] CustomerBasket updatedBasket)
        {
            var basketModel = await _basketRepository.GetOneAsync(basketId);

            if (basketModel is null)
            {
                return NotFound();
            }


            basketModel.UpdatedAt = DateTime.Now;
            basketModel.BasketProducts = updatedBasket.BasketProducts;
            basketModel.CustomerId = updatedBasket.CustomerId;




            await _basketRepository.UpdateAsync(basketModel.Id, basketModel);

            return NoContent();
        }

        // DELETE api/<BasketsController>/5
        [HttpDelete("{basketId:length(24)}")]
        public async Task<IActionResult> Delete(string basketId)
        {

            var basketModel = await _basketRepository.GetOneAsync(basketId);

            if (basketModel is null)
            {
                return NotFound();
            }

            await _basketRepository.RemoveAsync(basketModel.Id);

            return NoContent();
        }


        //TODO GET Basket - GET Baskets - PUT Basket - DELETE Basket - GET BASKET BY CUSTID
        //TODO CheckoutController - Get Basket Id from request body - Get basket from db - build payment intent 


    }
}
