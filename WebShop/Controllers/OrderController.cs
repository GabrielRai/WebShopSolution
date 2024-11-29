using Microsoft.AspNetCore.Mvc;
using Repository.Classes;
using Repository.Models;
using Repository.Services;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly OrderServices _orderService;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderService = new OrderServices(unitOfWork);
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

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest order)
        {
            if (order == null)
            {
                return BadRequest(new { Message = "Request is null." });
            }

            try
            {
                var result = _orderService.CreateOrderAsync(order);
                if (result.Result)
                {
                    return Ok(new { Message = "Order created successfully." });
                }
                return BadRequest(new { Message = "Failed to create order. Check stock or customer validity." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
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
