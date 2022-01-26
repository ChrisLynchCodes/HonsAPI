using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productsRepository;
        private readonly ICategoryRepository _categoriessRepository;
        private readonly IMapper _mapper;

        public ProductsController(ProductRepository productsRepository, IMapper mapper, CategoryRepository categoryRepository)
        {
            _productsRepository = productsRepository;
            _categoriessRepository = categoryRepository;
            _mapper = mapper;
        }



        // GET: api/<ProductsController>
        [HttpGet]
        [APIKey]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var productModels = await _productsRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productModels));

        }
        

        // GET: api/<ProductsController>
        [HttpGet]
        [APIKey]
        [Route("[action]/{ammount}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetN(int ammount)
        {
            var productModels = await _productsRepository.GetNAsync(ammount);

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productModels));

        }


        // GET api/<ProductsController>/5
        [HttpGet("{productId:length(24)}")]
        [APIKey]
        public async Task<ActionResult<ProductDto>> Get(string productId)
        {
            var productModel = await _productsRepository.GetOneAsync(productId);

            if (productModel is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(productModel));
        }


        // GET: api/<ProductsController>
        [HttpGet]
        [APIKey]
        [Route("[action]/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategoryAsync(string categoryId)
        {
            var productModels = await _productsRepository.GetProductsByCategoryAsync(categoryId);

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productModels));

        }



        // POST api/<ProductsController>
        [HttpPost("{categoryId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Post(string categoryId, [FromBody] ProductCreateDto newProduct)
        {
            var categoryModel = await _categoriessRepository.GetOneAsync(categoryId);
            if(categoryModel is null || categoryModel.Id is null)
            {
                return NotFound();
            }

            

            var productModel = _mapper.Map<Product>(newProduct);
            productModel.CategoryId = categoryModel.Id;

            await _productsRepository.CreateAsync(productModel);

            var productDto = _mapper.Map<ProductDto>(productModel);

            return CreatedAtAction(nameof(Get), new { id = productDto.Id }, productDto);
        }




        // PUT api/<CategoriesController>/5
        [HttpPut("{productId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Update(string productId, [FromBody] ProductCreateDto updatedProduct)
        {
            var productModel = await _productsRepository.GetOneAsync(productId);

            if (productModel is null)
            {
                return NotFound();
            }

            var productToSave = _mapper.Map<Product>(updatedProduct);

            productToSave.Id = productModel.Id;
            productToSave.UpdatedAt = DateTime.Now;
            productToSave.CreatedAt = productModel.CreatedAt;
            if (productToSave.Id is null)
            {
                return NotFound();
            }


            await _productsRepository.UpdateAsync(productToSave.Id, productToSave);

            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{productId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(string productId)
        {
            var productModel = await _productsRepository.GetOneAsync(productId);

            if (productModel is null || productModel.Id is null)
            {
                return NotFound();
            }

            await _productsRepository.RemoveAsync(productModel.Id);

            return NoContent();
        }


        [HttpOptions]
        public  IActionResult GetAuthorsOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,PUT,DEL");
            return Ok();
        }
    }
}
