using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
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
            if (orderItem == null)
                return BadRequest("OrderItem is null.");

            try
            {
                _unitOfWork.OrderItems.Add(orderItem);
                _unitOfWork.Complete();

                return Ok("OrderItem added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetOrderItems()
        {
            try
            {
                var orderItems = _unitOfWork.OrderItems.GetAll();

                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetOrderItemById(int id)
        {
            try
            {
                var orderItems = _unitOfWork.OrderItems.GetById(id);

                return Ok(orderItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut]
        public IActionResult UpdateOrderItem([FromBody] OrderItem orderItem)
        {
            if (orderItem == null)
                return BadRequest("OrderItem is null.");

            try
            {
                _unitOfWork.OrderItems.Update(orderItem);
                _unitOfWork.Complete();

                return Ok("OrderItem updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        public IActionResult DeleteOrderItem(int id)
        {
            try
            {
                var orderItem = _unitOfWork.OrderItems.GetById(id);

                if (orderItem == null)
                    return NotFound();

                _unitOfWork.OrderItems.Delete(orderItem);
                _unitOfWork.Complete();

                return Ok("OrderItem deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
