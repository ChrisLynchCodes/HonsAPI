using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;    
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewsController : ControllerBase
    {

        private readonly IReviewRepository _reviewRepository;
        private readonly ICustomerRepository _customersRepository;
        private readonly IProductRepository _productsRepository;
        private readonly IMapper _mapper;

        public ReviewsController(ReviewRepository reviewRepository, CustomerRepository customersRepository, ProductRepository productRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _productsRepository = productRepository;
            _customersRepository = customersRepository;
            _mapper = mapper;
        }




        // GET: api/<ReviewsController>  Get All reviews
        [HttpGet]
        [APIKey]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> Get()
        {
            var reviewModels = await _reviewRepository.GetAllAsync();

            if (reviewModels is null )
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviewModels));


        }



        // GET api/<ReviewsController>/5  Get Specific review
        [HttpGet("{reviewId:length(24)}")]
        [APIKey]
        public async Task<ActionResult<ReviewDto>> Get(string reviewId)
        {
            //Ensure review is valid
            var reviewModel = await _reviewRepository.GetOneAsync(reviewId);

            if (reviewModel is null || reviewModel.Id is null)
            {
                return NotFound();
            }

            

            //Map model to output DTO & return
            return Ok(_mapper.Map<Review>(reviewModel));


        }



        // GET: api/<ReviewsController> //Get All reviews for a specific product
        [HttpGet]
        [Route("[action]/{productId}")]
        [APIKey]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviewsForProduct(string productId)
        {
            var reviewForProductModels = await _reviewRepository.GetReviewsForProductAsync(productId);

            if (reviewForProductModels is null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviewForProductModels));


        }

        //Get all reviews by a customer

        // GET: api/<ReviewsController>
        [HttpGet]
        [Route("[action]/{customerId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviewsByCustomer(string customerId)
        {
            var reviewByCustomerModels = await _reviewRepository.GetReviewsByCustomerAsync(customerId);

            if (reviewByCustomerModels is null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviewByCustomerModels));


        }




        // POST api/<ReviewsController>
        [HttpPost("{customerId}/{productId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Customer")]
        public async Task<IActionResult> Post(string customerId, string productId, [FromBody] ReviewCreateDto newReview)
        {
            //Ensure customerId is a valid customer
            var customerModel = await _customersRepository.GetOneAsync(customerId);
            if (customerModel is null || customerModel.Id is null)
            {
                return NotFound();
            }
            //Ensure productId is a valid product
            var productModel = await _productsRepository.GetOneAsync(productId);
            if (productModel is null || productModel.Id is null)
            {
                return NotFound();
            }


            //Map create DTO to model
            var reviewModel = _mapper.Map<Review>(newReview);


            //add the valid customer id to the addresses  customer id
            reviewModel.CustomerId = customerModel.Id;
            reviewModel.ProductId = productModel.Id;
            //Save model in db
            await _reviewRepository.CreateAsync(reviewModel);

            //Map model to output DTO
            var reviewDto = _mapper.Map<ReviewDto>(reviewModel);

            //Ensure valid

            if (reviewDto is null || reviewDto.Id is null)
            {
                return NotFound();
            }



            return CreatedAtAction(nameof(Get), new { id = reviewDto.Id, }, reviewDto);



        }


        // PUT api/<ReviewsController>/5
        [HttpPut("{reviewId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin, Customer")]
        public async Task<IActionResult> Update(string reviewId, [FromBody] ReviewCreateDto updatedReview)
        {
            var reviewModel = await _reviewRepository.GetOneAsync(reviewId);

            if (reviewModel is null)
            {
                return NotFound();
            }

            var reviewToSave = _mapper.Map<Review>(updatedReview);

            reviewToSave.Id = reviewModel.Id;
            reviewToSave.CustomerId = reviewModel.CustomerId;
            reviewToSave.ProductId = reviewModel.ProductId;
            reviewToSave.UpdatedAt = DateTime.Now;
            reviewToSave.CreatedAt = reviewModel.CreatedAt;
            if (reviewToSave.Id is null)
            {
                return NotFound();
            }


            await _reviewRepository.UpdateAsync(reviewToSave.Id, reviewToSave);

            return NoContent();
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{reviewId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(string reviewId)
        {
            var reviewModel = await _reviewRepository.GetOneAsync(reviewId);

            if (reviewModel is null || reviewModel.Id is null)
            {
                return NotFound();
            }
            


            await _reviewRepository.RemoveAsync(reviewModel.Id);

            return NoContent();
        }
    }
}
