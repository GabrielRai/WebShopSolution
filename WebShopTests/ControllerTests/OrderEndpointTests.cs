using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebShop.Controllers;
using WebShop.UnitOfWork;

namespace WebShopTests.ControllerTests
{
    public class OrderEndpointTests
    {
        [Fact]
        public void GetAllOrders_ReturnsOkResult_WithAListOfOrders()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 1
                        }
                    }
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Id = 2,
                            OrderId = 2,
                            ProductId = 2,
                            Quantity = 2
                        }
                    }
                }
            };
            A.CallTo(() => fakeUoW.Orders.GetAll()).Returns(orders);

            // Act
            var controller = new OrderController(fakeUoW);
            var result = controller.GetOrders();

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Order>>(requestResult.Value);
            Assert.Equal(orders, returnValue);
        }

        [Fact]
        public void GetAllOrders_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();

            // Act
            var controller = new OrderController(fakeUoW);
            var result = controller.GetOrders();

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public void AddOrder_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var order = new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var controller = new OrderController(fakeUoW);
            var result = controller.AddOrder(order);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Orders.Add(A<Order>._)).MustHaveHappened();
        }

        [Fact]
        public void AddOrder_ReturnsBadRequest()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var order = new Order();
            var controller = new OrderController(fakeUoW);
            controller.ModelState.AddModelError("OrderId", "Required");

            // Act

            var result = controller.AddOrder(order);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Orders.Add(A<Order>._)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateOrder_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var order = new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var controller = new OrderController(fakeUoW);
            var result = controller.UpdateOrder(order);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Orders.Update(A<Order>._)).MustHaveHappened();
        }

        [Fact]
        public void UpdateOrder_ReturnsBadRequest()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var order = new Order();
            var controller = new OrderController(fakeUoW);
            controller.ModelState.AddModelError("OrderId", "Required");

            // Act
            var result = controller.UpdateOrder(order);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Orders.Update(A<Order>._)).MustNotHaveHappened();
        }

        [Fact]
        public void DeleteOrder_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var order = new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 1,
                        OrderId = 1,
                        ProductId = 1,
                        Quantity = 1
                    }
                }
            };

            var controller = new OrderController(fakeUoW);
            A.CallTo(() => fakeUoW.Orders.GetById(1)).Returns(order);

            // Act

            var result = controller.DeleteOrder(1);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Orders.Delete(A<Order>._)).MustHaveHappened();
        }

        [Fact]
        public void DeleteOrder_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var order = new Order();

            var controller = new OrderController(fakeUoW);
            A.CallTo(() => fakeUoW.Orders.GetById(1)).Returns(null);

            // Act

            var result = controller.DeleteOrder(1);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Orders.Delete(A<Order>._)).MustNotHaveHappened();
        }
    }
}
