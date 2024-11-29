using Moq;
using Repository.Models;
using Repository.Repositories.Orders;

namespace WebShopTests.UnitTests
{
    public class OrderUnitTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        public OrderUnitTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
        }

        [Fact]
        public void GetOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1, OrderDate = DateTime.Now,
                    Customer = new Customer { Id = 1, Name = "John" }
                },
                new Order
                {
                    Id = 2, OrderDate = DateTime.Now,
                    Customer = new Customer { Id = 2, Name = "Jane" }
                }
            };
            _mockOrderRepository.Setup(x => x.GetAll()).Returns(orders);

            // Act
            var result = _mockOrderRepository.Object.GetAll();

            // Assert
            Assert.Equal(orders, result);
        }

        [Fact]
        public void GetOrders_ReturnsNull()
        {
            // Arrange
            _mockOrderRepository.Setup(x => x.GetAll()).Returns(() => null);

            // Act
            var result = _mockOrderRepository.Object.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOrderById_ReturnsOkResult_WithAnOrder()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Customer = new Customer { Id = 1, Name = "John" }
            };
            _mockOrderRepository.Setup(x => x.GetById(order.Id)).Returns(order);

            // Act
            var result = _mockOrderRepository.Object.GetById(order.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order, result);
        }

        [Fact]
        public void GetOrderById_ReturnsNull()
        {

            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Customer = new Customer { Id = 1, Name = "John" }
            };
            _mockOrderRepository.Setup(x => x.GetById(order.Id)).Returns(() => null);

            // Act
            var result = _mockOrderRepository.Object.GetById(order.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdateOrder_ReturnsOkResult()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Customer = new Customer { Id = 1, Name = "John" }
            };
            _mockOrderRepository.Setup(x => x.Update(order));

            // Act
            _mockOrderRepository.Object.Update(order);

            // Assert
            _mockOrderRepository.Verify(x => x.Update(order), Times.Once);
        }

        [Fact]
        public void UpdateOrder_ReturnsNoResult()
        {
            // Arrange
            Order order = null;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockOrderRepository.Object.Update(order));
        }

        [Fact]
        public void DeleteOrder_ReturnsOkResult()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Customer = new Customer { Id = 1, Name = "John" }
            };
            _mockOrderRepository.Setup(repo => repo.Delete(order));

            // Act
            _mockOrderRepository.Object.Delete(order);
            var result = _mockOrderRepository.Object.GetById(order.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DeleteOrder_ReturnsNoResult()
        {
            // Arrange
            Order order = null;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockOrderRepository.Object.Delete(order));
        }

        [Fact]
        public void CreateOrder_ReturnsOkResult()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Customer = new Customer { Id = 1, Name = "John" },
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 },
                    new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 2 }
                }
            };
            _mockOrderRepository.Setup(repo => repo.CreateOrder(order)).Returns(true);

            // Act
            var result = _mockOrderRepository.Object.CreateOrder(order);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CreateOrder_ReturnsNoResult()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                OrderDate = DateTime.Now,
                Customer = new Customer { Id = 1, Name = "John" },
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 },
                    new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 2 }
                }
            };
            _mockOrderRepository.Setup(repo => repo.CreateOrder(order)).Returns(false);

            // Act
            var result = _mockOrderRepository.Object.CreateOrder(order);

            // Assert
            Assert.False(result);
        }

    }
}
