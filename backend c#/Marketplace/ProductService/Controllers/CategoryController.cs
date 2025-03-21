using Microsoft.AspNetCore.Mvc;
using ProductService.Model.DTO;
using ProductService.Model.Mapper;
using ProductService.Model.Service;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("/api/v1/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => 
            _categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(CategoryMapper.MapCategoryDTOListToCategoryViewList(await _categoryService.GetAll()));
        }

        [HttpGet]
        [Route("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryDTO? currentCategoryDTO = await _categoryService.GetById(id);

            if(currentCategoryDTO == null) 
                return NotFound("Category Not Found.");
               
            return Ok(CategoryMapper.MapCategoryDTOToCategoryView(currentCategoryDTO));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.Create(categoryDTO);
            return Created("categories/create", CategoryMapper.MapCategoryDTOToCategoryView(categoryDTO));
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDTO categoryDTO)
        {
            CategoryDTO? currentCategoryDTO = await _categoryService.GetById(id);

            if (currentCategoryDTO == null)
                return NotFound("Category Not Found.");

            await _categoryService.Update(id, categoryDTO);
            return Ok(CategoryMapper.MapCategoryDTOToCategoryView(categoryDTO));
        }

        [HttpDelete]
        [Route("delete/{id}")]
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
