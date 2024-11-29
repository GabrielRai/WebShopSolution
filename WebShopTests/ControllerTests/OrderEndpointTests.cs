using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Classes;
using Repository.Models;
using Repository.Repositories.Customers;
using Repository.Repositories.Orders;
using Repository.Repositories.Products;
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

        [Fact]
        public async Task CreateOrder_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var fakeCustomerRepo = A.Fake<ICustomerRepository>();
            var fakeProductRepo = A.Fake<IProductRepository>();
            var fakeOrderRepo = A.Fake<IOrderRepository>();

            A.CallTo(() => fakeUoW.Customers).Returns(fakeCustomerRepo);
            A.CallTo(() => fakeUoW.Products).Returns(fakeProductRepo);
            A.CallTo(() => fakeUoW.Orders).Returns(fakeOrderRepo);

            A.CallTo(() => fakeCustomerRepo.GetById(1))
                .Returns(new Customer { Id = 1, Name = "Test Customer" });

            A.CallTo(() => fakeProductRepo.GetById(1))
                .Returns(new Product { Id = 1, Stock = 10, Name = "Test Product" });

            A.CallTo(() => fakeOrderRepo.CreateOrder(A<Order>._)).Returns(true);

            var order = new CreateOrderRequest
            {
                CustomerId = 1,
                OrderDate = DateTime.Now,
                Products = new List<ProductRequest>
                {
                    new ProductRequest { ProductId = 1, Quantity = 1 }
                }
            };

            var controller = new OrderController(fakeUoW);

            // Act
            var result = controller.CreateOrder(order);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);

            A.CallTo(() => fakeUoW.Orders.CreateOrder(A<Order>._)).MustHaveHappened();
            A.CallTo(() => fakeUoW.Complete()).MustHaveHappened();
        }
    }
}
