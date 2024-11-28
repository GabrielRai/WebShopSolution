using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IActionResult AddOrderItem([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.OrderItems.Add(orderItem);
            _unitOfWork.Complete();

            return Ok("OrderItem added successfully.");
        }

        [HttpGet]
        public IActionResult GetOrderItems()
        {
            var orderItems = _unitOfWork.OrderItems.GetAll();

            if (!orderItems.Any())
            {
                return NotFound(new { Message = "OrderItems not found" });
            }
            return Ok(orderItems);

        }
        [HttpGet("{id}")]
        public IActionResult GetOrderItemById(int id)
        {
            var orderItems = _unitOfWork.OrderItems.GetById(id);

            if (orderItems == null)
            {
                return NotFound(new { Message = "OrderItem not found" });
            }

            return Ok(orderItems);
        }
        [HttpPut]
        public IActionResult UpdateOrderItem([FromBody] OrderItem orderItem)
        {
            var existingOrderItem = _unitOfWork.OrderItems.GetById(orderItem.Id);

            if (existingOrderItem == null)
            {
                return NotFound(new { Message = "OrderItem not found" });
            }

            _unitOfWork.OrderItems.Update(orderItem);
            _unitOfWork.Complete();

            return Ok("OrderItem updated successfully.");

        }
        [HttpDelete]
        public IActionResult DeleteOrderItem(int id)
        {

            var orderItem = _unitOfWork.OrderItems.GetById(id);

            if (orderItem == null)
            {
                return NotFound(new { Message = "OrderItem not found" });
            }

            _unitOfWork.OrderItems.Delete(orderItem);
            _unitOfWork.Complete();

            return Ok("OrderItem deleted successfully.");

        }
    }
}
