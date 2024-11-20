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
            if (category == null)
                return BadRequest("Category is null.");

            try
            {
                _unitOfWork.Categories.Add(category);

                // Save changes
                _unitOfWork.Complete();

                return Ok("Category added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _unitOfWork.Categories.GetAll();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var category = _unitOfWork.Categories.GetById(id);

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        public IActionResult UpdateCategory([FromBody] Category category)
        {
            if (category == null)
                return BadRequest("Category is null.");

            try
            {
                _unitOfWork.Categories.Update(category);
                _unitOfWork.Complete();

                return Ok("Category updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = _unitOfWork.Categories.GetById(id);

                if (category == null)
                    return NotFound();

                _unitOfWork.Categories.Delete(category);
                _unitOfWork.Complete();

                return Ok("Category deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
