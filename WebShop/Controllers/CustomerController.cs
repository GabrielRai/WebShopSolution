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
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.Customers.Add(customer);
            _unitOfWork.Complete();

            return Ok("Customer added successfully.");
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = _unitOfWork.Customers.GetAll();

            if (!customers.Any())
            {
                return NotFound(new { Message = "Customers not found" });
            }

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customers = _unitOfWork.Customers.GetById(id);

            if (customers == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            return Ok(customers);
        }

        [HttpPut]
        public IActionResult UpdateCustomer([FromBody] Customer customer)
        {
            var existingCustomer = _unitOfWork.Customers.GetById(customer.Id);
            if (existingCustomer == null)
            {
                return NotFound(new { Message = $"Customer with ID {customer.Id} not found." });
            }
       
            _unitOfWork.Customers.Update(customer);
            _unitOfWork.Complete();

            return Ok("Customer updated successfully.");
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _unitOfWork.Customers.GetById(id);
            if (customer == null)
            {
                return NotFound(new { Message = "Customer not found" });
            }

            _unitOfWork.Customers.Delete(customer);
            _unitOfWork.Complete();

            return Ok("Customer deleted successfully.");
        }
    }
}
