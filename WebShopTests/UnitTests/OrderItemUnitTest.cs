using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Repository.Models;
using Repository.Repositories.OrderItems;

namespace WebShopTests.UnitTests
{
    public class OrderItemUnitTest
    {
        private readonly Mock<IOrderItemRepository> _mockOrderRepository;
        public OrderItemUnitTest()
        {
            _mockOrderRepository = new Mock<IOrderItemRepository>();
        }

        [Fact]
        public void GetAllOrderItems_ReturnsOkResult_WithAListOfOrderItems()
        {
            // Arrange
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 },
                new OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 2 },
                new OrderItem { Id = 3, OrderId = 2, ProductId = 3, Quantity = 3 }
            };
            _mockOrderRepository.Setup(repo => repo.GetAll()).Returns(orderItems);

            // Act
            var result = _mockOrderRepository.Object.GetAll();

            // Assert
            Assert.Equal(orderItems, result);
        }

        [Fact]
        public void GetAllOrderItems_ReturnsNull()
        {
            // Arrange
            _mockOrderRepository.Setup(repo => repo.GetAll()).Returns(() => null);

            // Act
            var result = _mockOrderRepository.Object.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetOrderItemById_ReturnsOkResult_WithAOrderItem()
        {
            // Arrange
            var orderItem = new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 };
            _mockOrderRepository.Setup(repo => repo.GetById(orderItem.Id)).Returns(orderItem);

            // Act
            var result = _mockOrderRepository.Object.GetById(orderItem.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderItem, result);
        }

        [Fact]
        public void GetOrderItemById_ReturnsNull()
        {
            // Arrange
            var orderItem = new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 };
            _mockOrderRepository.Setup(repo => repo.GetById(orderItem.Id)).Returns(() => null);

            // Act
            var result = _mockOrderRepository.Object.GetById(orderItem.Id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddOrderItem_ReturnsOkResult()
        {
            // Arrange
            var orderItem = new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 };
            _mockOrderRepository.Setup(repo => repo.Add(orderItem));

            // Act
            var result = _mockOrderRepository.Object.Add(orderItem);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void AddOrderItem_ReturnsNull()
        {
            // Arrange
            OrderItem orderItem = null;

            // Assert & Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockOrderRepository.Object.Add(orderItem));
        }

        [Fact]
        public void UpdateOrderItem_ReturnsOkResult()
        {
            // Arrange
            var orderItem = new OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 1 };
            _mockOrderRepository.Setup(repo => repo.Update(orderItem));

            // Act
            _mockOrderRepository.Object.Update(orderItem);

            // Assert
            _mockOrderRepository.Verify(repo => repo.Update(It.Is<OrderItem>(o => o.Id == orderItem.Id)), Times.Once);
        }

        [Fact]
        public void UpdateOrderItem_ThrowsException_WhenProductIsNull()
        {
            // Arrange
            OrderItem orderItem = null;

            // Assert & Act
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockOrderRepository.Object.Update(orderItem));
        }
    }
}
