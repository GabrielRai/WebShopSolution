using Microsoft.AspNetCore.Mvc;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.Orders.Add(order);
            _unitOfWork.Complete();

            return Ok("Order added successfully.");
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _unitOfWork.Orders.GetAll();

            if (!orders.Any())
            {
                return NotFound(new { Message = "Orders not found" });
            }

            return Ok(orders);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {

            var orders = _unitOfWork.Orders.GetById(id);

            if (orders == null)
            {
                return NotFound(new { Message = "Order not found" });
            }

            return Ok(orders);

        }
        [HttpPut]
        public IActionResult UpdateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.Orders.Update(order);
            _unitOfWork.Complete();

            return Ok("Order updated successfully.");
        }
        [HttpDelete]
        public IActionResult DeleteOrder(int id)
        {

            var order = _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with ID {id} not found." });
            }

            _unitOfWork.Orders.Delete(order);
            _unitOfWork.Complete();

            return Ok("Order deleted successfully.");

        }
    }
}
