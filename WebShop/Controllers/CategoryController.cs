using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Category is null.");
            }

            _unitOfWork.Categories.Add(category);
            _unitOfWork.Complete();

            return Ok("Category added successfully.");
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categories = _unitOfWork.Categories.GetAll();

            if (!categories.Any())
            {
                return NotFound(new { Message = "Categories not found" });
            }
            return Ok(categories);

        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);

            if (category == null)
            {
                return NotFound(new { Message = "Category not found" });
            }

            return Ok(category);
        }

        [HttpPut]
        public IActionResult UpdateCategory([FromBody] Category category)
        {

            var existingCategory = _unitOfWork.Categories.GetById(category.Id);
            if (existingCategory == null)
            {
                return NotFound(new { Message = $"Category with ID {category.Id} not found." });
            }

            _unitOfWork.Categories.Update(category);
            _unitOfWork.Complete();

            return Ok("Category updated successfully.");

        }

        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var category = _unitOfWork.Categories.GetById(id);

            if (category == null)
            {
                return NotFound(new { Message = "Category not found" });
            }

            _unitOfWork.Categories.Delete(category);
            _unitOfWork.Complete();

            return Ok("Category deleted successfully.");
        }
    }
}
