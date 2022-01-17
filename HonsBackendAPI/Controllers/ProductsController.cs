using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services;
using HonsBackendAPI.Services.Repositories;
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
        [HttpHead]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _productsRepository.GetAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));

        }


        // GET api/<ProductsController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ProductDto>> Get(string id)
        {
            var product = await _productsRepository.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(product));
        }




        // POST api/<ProductsController>
        [HttpPost("{categoryId}")]
        public async Task<IActionResult> Post(string categoryId, [FromBody] ProductCreateDto newProduct)
        {
            var category = await _categoriessRepository.GetAsync(categoryId);
            if(category is null || category.Id is null)
            {
                return NotFound();
            }

            

            var productModel = _mapper.Map<Product>(newProduct);
            productModel.CategoryId = category.Id;

            await _productsRepository.CreateAsync(productModel);

            var productDto = _mapper.Map<ProductDto>(productModel);

            return CreatedAtAction(nameof(Get), new { id = productDto.Id }, productDto);
        }




        // PUT api/<CategoriesController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProductCreateDto updatedProduct)
        {
            var product = await _productsRepository.GetAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            var productToSave = _mapper.Map<Product>(updatedProduct);

            productToSave.Id = product.Id;
            productToSave.UpdatedAt = DateTime.Now;
            productToSave.CreatedAt = product.CreatedAt;
            if (productToSave.Id is null)
            {
                return NotFound();
            }


            await _productsRepository.UpdateAsync(productToSave.Id, productToSave);

            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productsRepository.GetAsync(id);

            if (product is null || product.Id is null)
            {
                return NotFound();
            }

            await _productsRepository.RemoveAsync(product.Id);

            return NoContent();
        }
    }
}
