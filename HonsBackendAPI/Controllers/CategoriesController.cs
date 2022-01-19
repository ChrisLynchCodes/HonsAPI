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
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categoryModels = await _categoriesRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categoryModels));

        }


        // GET api/<CategoriesController>/5
        [HttpGet("{categoryId:length(24)}")]
        public async Task<ActionResult<CategoryDto>> Get(string categoryId)
        {
            var categoryModel = await _categoriesRepository.GetOneAsync(categoryId);

            if (categoryModel is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CategoryDto>(categoryModel));
        }



        // POST api/<CategoriesController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto newCategory)
        {
            var categoryModel = _mapper.Map<Category>(newCategory);

            await _categoriesRepository.CreateAsync(categoryModel);

            var categoryDto = _mapper.Map<CategoryDto>(categoryModel);

            return CreatedAtAction(nameof(Get), new { categoryName = newCategory.CategoryName }, categoryDto);
        }



        // PUT api/<CategoriesController>/5
        [HttpPut("{categoryId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Update(string categoryid, [FromBody] CategoryCreateDto updatedCategory)
        {
            var categoryModel = await _categoriesRepository.GetOneAsync(categoryid);

            if (categoryModel is null)
            {
                return NotFound();
            }

            var categoryToSave = _mapper.Map<Category>(updatedCategory);
          
            categoryToSave.Id = categoryModel.Id;
            categoryToSave.UpdatedAt = DateTime.Now;
            categoryToSave.CreatedAt = categoryModel.CreatedAt;

            if (categoryToSave.Id is null)
            {
                return NotFound();
            }


            await _categoriesRepository.UpdateAsync(categoryToSave.Id, categoryToSave);

            return NoContent();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{categoryId:length(24)}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin, Admin")]
        public async Task<IActionResult> Delete(string categoryId)
        {
            var categoryModel = await _categoriesRepository.GetOneAsync(categoryId);

            if (categoryModel is null || categoryModel.Id is null)
            {
                return NotFound();
            }

            await _categoriesRepository.RemoveAsync(categoryModel.Id);

            return NoContent();
        }


    }
}
