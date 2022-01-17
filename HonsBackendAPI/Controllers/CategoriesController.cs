using AutoMapper;
using HonsBackendAPI.Attributes;
using HonsBackendAPI.DTOs;
using HonsBackendAPI.Models;
using HonsBackendAPI.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HonsBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [APIKey]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoriesRepository;
        private readonly IMapper _mapper;

        public CategoriesController(CategoryRepository categoriesRepository, IMapper mapper)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
        }




        // GET: api/<CategoriesController>
        [HttpGet]
        [HttpHead]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categories = await _categoriesRepository.GetAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));

        }


        // GET api/<CategoriesController>/5
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<CategoryDto>> Get(string id)
        {
            var categories = await _categoriesRepository.GetAsync(id);

            if (categories is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(categories));
        }



        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto newCategory)
        {
            var categoryModel = _mapper.Map<Category>(newCategory);

            await _categoriesRepository.CreateAsync(categoryModel);

            var categoryDto = _mapper.Map<CategoryDto>(categoryModel);

            return CreatedAtAction(nameof(Get), new { categoryName = newCategory.CategoryName }, categoryDto);
        }



        // PUT api/<CategoriesController>/5
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryCreateDto updatedCategory)
        {
            var category = await _categoriesRepository.GetAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            var categoryToSave = _mapper.Map<Category>(updatedCategory);
          
            categoryToSave.Id = category.Id;
            categoryToSave.UpdatedAt = DateTime.Now;
            categoryToSave.CreatedAt = category.CreatedAt;
            if (categoryToSave.Id is null)
            {
                return NotFound();
            }


            await _categoriesRepository.UpdateAsync(categoryToSave.Id, categoryToSave);

            return NoContent();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var categories = await _categoriesRepository.GetAsync(id);

            if (categories is null || categories.Id is null)
            {
                return NotFound();
            }

            await _categoriesRepository.RemoveAsync(categories.Id);

            return NoContent();
        }


    }
}
