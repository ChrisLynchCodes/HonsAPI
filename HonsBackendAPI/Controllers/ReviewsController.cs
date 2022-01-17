using AutoMapper;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Http;    
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpHead]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> Get()
        {
            var reviews = await _reviewRepository.GetAsync();

            if (reviews is null )
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));


        }



        // GET api/<ReviewsController>/5  Get Specific review
        [HttpGet("{reviewId:length(24)}")]
        public async Task<ActionResult<ReviewDto>> Get(string reviewId)
        {
            //Ensure review is valid
            var review = await _reviewRepository.GetAsync(reviewId);

            if (review is null || review.Id is null)
            {
                return NotFound();
            }

            

            //Map model to output DTO & return
            return Ok(_mapper.Map<Review>(review));


        }



        // GET: api/<ReviewsController> //Get All reviews for a specific product
        [HttpGet]
        [Route("[action]/{productId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviewsForProduct(string productId)
        {
            var reviews = await _reviewRepository.GetReviewsForProductAsync(productId);

            if (reviews is null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));


        }

        //Get all reviews by a customer

        // GET: api/<ReviewsController>
        [HttpGet]
        [Route("[action]/{customerId}")]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAllReviewsByCustomer(string customerId)
        {
            var reviews = await _reviewRepository.GetReviewsForCustomerAsync(customerId);

            if (reviews is null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<ReviewDto>>(reviews));


        }




        // POST api/<ReviewsController>
        [HttpPost("{customerId}/{productId}")]
        public async Task<IActionResult> Post(string customerId, string productId, [FromBody] ReviewCreateDto newReview)
        {
            //Ensure customerId is a valid customer
            var customer = await _customersRepository.GetAsync(customerId);
            if (customer is null || customer.Id is null)
            {
                return NotFound();
            }
            //Ensure productId is a valid product
            var product = await _productsRepository.GetAsync(productId);
            if (product is null || product.Id is null)
            {
                return NotFound();
            }


            //Map create DTO to model
            var reviewModel = _mapper.Map<Review>(newReview);


            //add the valid customer id to the addresses  customer id
            reviewModel.CustomerId = customer.Id;
            reviewModel.ProductId = product.Id;
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
        public async Task<IActionResult> Update(string reviewId, [FromBody] ReviewCreateDto updatedReview)
        {
            var review = await _reviewRepository.GetAsync(reviewId);

            if (review is null)
            {
                return NotFound();
            }

            var reviewToSave = _mapper.Map<Review>(updatedReview);

            reviewToSave.Id = review.Id;
            reviewToSave.CustomerId = review.CustomerId;
            reviewToSave.ProductId = review.ProductId;
            reviewToSave.UpdatedAt = DateTime.Now;
            reviewToSave.CreatedAt = review.CreatedAt;
            if (reviewToSave.Id is null)
            {
                return NotFound();
            }


            await _reviewRepository.UpdateAsync(reviewToSave.Id, reviewToSave);

            return NoContent();
        }

        // DELETE api/<ReviewsController>/5
        [HttpDelete("{reviewId:length(24)}")]
        public async Task<IActionResult> Delete(string reviewId)
        {
            var review = await _reviewRepository.GetAsync(reviewId);

            if (review is null || review.Id is null)
            {
                return NotFound();
            }
            


            await _reviewRepository.RemoveAsync(review.Id);

            return NoContent();
        }
    }
}
