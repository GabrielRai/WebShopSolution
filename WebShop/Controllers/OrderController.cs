using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Order is null.");

            try
            {
                _unitOfWork.Orders.Add(order);

                // Save changes
                _unitOfWork.Complete();

                return Ok("Order added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            try
            {
                var orders = _unitOfWork.Orders.GetAll();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            try
            {
                var orders = _unitOfWork.Orders.GetById(id);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        public IActionResult UpdateOrder([FromBody] Order order)
        {
            if (order == null)
                return BadRequest("Order is null.");

            try
            {
                _unitOfWork.Orders.Update(order);
                _unitOfWork.Complete();

                return Ok("Order updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                var order = _unitOfWork.Orders.GetById(id);

                if (order == null)
                    return NotFound();

                _unitOfWork.Orders.Delete(order);
                _unitOfWork.Complete();

                return Ok("Order deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
