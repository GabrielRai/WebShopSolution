using Microsoft.AspNetCore.Mvc;
using Repository;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        // Constructor with UnitOfWork injected
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product is null.");

            try
            {
                _unitOfWork.Products.Add(product);

                // Save changes
                _unitOfWork.Complete();

                return Ok("Product added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Product
        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var products = _unitOfWork.Products.GetAll();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var product = _unitOfWork.Products.GetById(id);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            if (product == null)
                return BadRequest("Product is null.");

            try
            {
                _unitOfWork.Products.Update(product);
                _unitOfWork.Complete();

                return Ok("Product updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                var product = _unitOfWork.Products.GetById(id);

                if (product == null)
                    return NotFound();

                _unitOfWork.Products.Delete(product);
                _unitOfWork.Complete();

                return Ok("Product deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
