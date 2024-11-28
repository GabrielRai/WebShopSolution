using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebShop.Controllers;
using WebShop.UnitOfWork;

namespace WebShopTests.ControllerTests
{
    public class OrderItemEndpointTests
    {
        [Fact]
        public void GetAllOrderItems_ReturnsOkResult_WithAListOfOrderItems()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1,
                    Quantity = 1
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 2,
                    ProductId = 2,
                    Quantity = 2
                }
            };
            A.CallTo(() => fakeUoW.OrderItems.GetAll()).Returns(orderItems);

            // Act
            var controller = new OrderItemController(fakeUoW);
            var result = controller.GetOrderItems();

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<OrderItem>>(requestResult.Value);
            Assert.Equal(orderItems, returnValue);
        }
    }
}
