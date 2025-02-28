using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => 
            _categoryService = categoryService;

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet]
        [Route("categories/getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryDTO? currentCategoryDTO = await _categoryService.GetById(id);

            if(currentCategoryDTO == null) 
                return NotFound("Category Not Found.");
               
            return Ok(currentCategoryDTO);
        }

        [HttpPost]
        [Route("categories/create")]
        public async Task<IActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.Create(categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpPut]
        [Route("categories/update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO categoryDTO)
        {
            CategoryDTO? currentCategoryDTO = await _categoryService.GetById(id);

            if (currentCategoryDTO == null)
                return NotFound("Category Not Found.");

            await _categoryService.Update(id, categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpDelete]
        [Route("categories/delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            CategoryDTO? currentCategoryDTO = await _categoryService.GetById(id);

            if (currentCategoryDTO == null)
                return NotFound("Category Not Found.");

            await _categoryService.DeleteById(id);
            return NoContent();
        }
    }
}
