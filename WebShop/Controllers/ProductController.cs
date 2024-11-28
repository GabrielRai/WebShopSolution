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

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.Products.Add(product);
            _unitOfWork.Complete();

            return Ok("Product added successfully.");
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _unitOfWork.Products.GetAll();

            if (!products.Any())
            {
                return NotFound(new { Message = "Products not found" });
            }
            return Ok(products);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            
            var product = _unitOfWork.Products.GetById(id);

            if (product == null)
            {
                return NotFound(new { Message = "Product not found" });
            }

            return Ok(product);

        }
        [HttpPut]
        public IActionResult UpdateProduct([FromBody] Product product)
        {

            var existingProduct = _unitOfWork.Products.GetById(product.Id);
            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {product.Id} not found." });
            }

            _unitOfWork.Products.Update(product);
            _unitOfWork.Complete();

            return Ok("Product updated successfully.");
            
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            
            var product = _unitOfWork.Products.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Delete(product);
            _unitOfWork.Complete();

            return Ok("Product deleted successfully.");
        
        }
    }
}
