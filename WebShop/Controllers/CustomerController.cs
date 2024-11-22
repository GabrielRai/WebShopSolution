using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer is null.");

            try
            {
                _unitOfWork.Customers.Add(customer);
                _unitOfWork.Complete();

                return Ok("Customer added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            try
            {
                var customers = _unitOfWork.Customers.GetAll();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var customers = _unitOfWork.Customers.GetById(id);

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory([FromBody] Customer customer)
        {
            if (customer == null)
                return BadRequest("Customer is null.");

            try
            {
                _unitOfWork.Customers.Update(customer);
                _unitOfWork.Complete();

                return Ok("Customer updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var customer = _unitOfWork.Customers.GetById(id);

                if (customer == null)
                    return NotFound();

                _unitOfWork.Customers.Delete(customer);
                _unitOfWork.Complete();

                return Ok("Customer deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
