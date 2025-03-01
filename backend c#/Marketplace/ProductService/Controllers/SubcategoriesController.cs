using Microsoft.AspNetCore.Mvc;
using ProductService.DTO;
using ProductService.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/v1/subcategories")]
    public class SubcategoriesController : Controller
    {
        private readonly ISubcategoriesService _subcategoriesService;

        private readonly ICategoryService _categoryService;

        public SubcategoriesController(ISubcategoriesService subcategoriesService, 
            ICategoryService categoryService)
        {
            _subcategoriesService = subcategoriesService;
            _categoryService = categoryService;

        }
           
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _subcategoriesService.GetAll());
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            SubcategoriesDTO? currentSubcategoriesDTO = await _subcategoriesService.GetById(id);

            if(currentSubcategoriesDTO == null)
                return NotFound("Subcategories Not Found.");

            return Ok(currentSubcategoriesDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SubcategoriesDTO subcategoriesDTO)
        {
            CategoryDTO? currentCategoryDTO = await _categoryService.GetById(subcategoriesDTO.CategoryId);

            if(currentCategoryDTO != null)
            {
                await _subcategoriesService.Create(subcategoriesDTO);
                return Ok(subcategoriesDTO);
            }
            return BadRequest("Categories Not Found.");
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SubcategoriesDTO subcategoriesDTO)
        {
            SubcategoriesDTO? currentSubcategoriesDTO = await _subcategoriesService.GetById(id);
            CategoryDTO? currentCategoryDTO = await _categoryService.GetById(subcategoriesDTO.CategoryId);

            if (currentSubcategoriesDTO == null && currentCategoryDTO == null)
                return NotFound("Categories and Subcategories Not Found.");
            else if (currentCategoryDTO == null)
                return NotFound("Categories Not Found.");
            else if (currentSubcategoriesDTO == null)
                return NotFound("Subcategories Not Found.");

            await _subcategoriesService.Update(id, subcategoriesDTO);
            return Ok(subcategoriesDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            SubcategoriesDTO? currentSubcategoriesDTO = await _subcategoriesService.GetById(id);

            if (currentSubcategoriesDTO == null)
                return NotFound("Subcategories Not Found.");

            await _subcategoriesService.DeleteById(id);
            return NoContent();
        }
    }
}
