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

        [Fact]
        public void GetAllOrderItems_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();

            // Act
            var controller = new OrderItemController(fakeUoW);
            var result = controller.GetOrderItems();

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public void AddOrderItems_ReturnOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orderItem = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1
            };

            // Act
            var controller = new OrderItemController(fakeUoW);
            var result = controller.AddOrderItem(orderItem);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.OrderItems.Add(A<OrderItem>._)).MustHaveHappened();
        }

        [Fact]
        public void AddOrderItems_ReturnsBadRequest()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orderItem = new OrderItem();

            var controller = new OrderItemController(fakeUoW);
            controller.ModelState.AddModelError("ProductId", "Required");

            // Act
            var result = controller.AddOrderItem(orderItem);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.OrderItems.Add(A<OrderItem>._)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateOrderItem_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orderItem = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1
            };

            // Act
            var controller = new OrderItemController(fakeUoW);
            var result = controller.UpdateOrderItem(orderItem);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.OrderItems.Update(A<OrderItem>._)).MustHaveHappened();
        }

        [Fact]
        public void UpdateOrderItem_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orderItem = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1
            };

            var controller = new OrderItemController(fakeUoW);
            A.CallTo(() => fakeUoW.OrderItems.GetById(1)).Returns(null);

            // Act
            var result = controller.UpdateOrderItem(orderItem);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.OrderItems.Update(A<OrderItem>._)).MustNotHaveHappened();
        }

        [Fact]
        public void DeleteOrderItem_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orderItem = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1
            };

            var controller = new OrderItemController(fakeUoW);
            A.CallTo(() => fakeUoW.OrderItems.GetById(1)).Returns(orderItem);

            // Act
            var result = controller.DeleteOrderItem(1);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.OrderItems.Delete(A<OrderItem>._)).MustHaveHappened();
        }

        [Fact]
        public void DeleteOrderItem_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orderItem = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 1
            };

            var controller = new OrderItemController(fakeUoW);
            A.CallTo(() => fakeUoW.OrderItems.GetById(1)).Returns(null);

            // Act
            var result = controller.DeleteOrderItem(1);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.OrderItems.Delete(A<OrderItem>._)).MustNotHaveHappened();
        }
    }
}
