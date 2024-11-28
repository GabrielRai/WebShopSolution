using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Repository.Models;
using Repository.Repositories.Customers;

namespace WebShopTests.UnitTests
{
    public class CustomerUnitTests
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        public CustomerUnitTests()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
        }
        [Fact]
        public void GetCustomers_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1, Name = "John",
                    Orders = new List<Order>
                    {
                        new Order { Id = 1, OrderDate = DateTime.Now },
                        new Order { Id = 2, OrderDate = DateTime.Now }
                    }
                },
                new Customer
                {
                    Id = 2, 
                    Name = "Jane",
                    Orders = new List<Order>
                    {
                        new Order { Id = 3, OrderDate = DateTime.Now },
                        new Order { Id = 4, OrderDate = DateTime.Now }
                    }

                }
            };
            _mockCustomerRepository.Setup(x => x.GetAll()).Returns(customers);

            // Act
            var result = _mockCustomerRepository.Object.GetAll();

            // Assert
            Assert.Equal(customers, result);
        }

        [Fact]
        public void GetCustomers_ReturnsNull() {
            // Arrange
            _mockCustomerRepository.Setup(x => x.GetAll()).Returns(() => null);

            // Act
            var result = _mockCustomerRepository.Object.GetAll();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCustomerById_ReturnsOkResult_WithACustomer()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "John",
                Orders = new List<Order>
                {
                    new Order { Id = 1, OrderDate = DateTime.Now },
                    new Order { Id = 2, OrderDate = DateTime.Now }
                }
            };
            _mockCustomerRepository.Setup(x => x.GetById(customer.Id)).Returns(customer);

            // Act
            var result = _mockCustomerRepository.Object.GetById(customer.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer, result);
        }

        [Fact]
        public void GetCustomerById_ReturnsNull() {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "John",
                Orders = new List<Order>
                {
                    new Order { Id = 1, OrderDate = DateTime.Now },
                    new Order { Id = 2, OrderDate = DateTime.Now }
                }
            };
            _mockCustomerRepository.Setup(x => x.GetById(customer.Id)).Returns(() => null);

            // Act
            var result = _mockCustomerRepository.Object.GetById(customer.Id);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void AddCustomer_ReturnsOkResult()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "John",
                Orders = new List<Order>
                {
                    new Order { Id = 1, OrderDate = DateTime.Now },
                    new Order { Id = 2, OrderDate = DateTime.Now }
                }
            };
            _mockCustomerRepository.Setup(x => x.Add(customer));

            // Act
            _mockCustomerRepository.Object.Add(customer);

            // Assert

            _mockCustomerRepository.Verify(repo => repo.Add(It.Is<Customer>(c => c.Id == customer.Id && c.Name == customer.Name)));
        }

        [Fact]
        public void AddCustomer_ThrowsException_WhenCustomerIsNull()
        {
            // Arrange
            Customer customer = null;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockCustomerRepository.Object.Add(customer));
        }

        [Fact]
        public void UpdateCustomer_ReturnsOkResult()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "John",
                Orders = new List<Order>
                {
                    new Order { Id = 1, OrderDate = DateTime.Now },
                    new Order { Id = 2, OrderDate = DateTime.Now }
                }
            };
            _mockCustomerRepository.Setup(x => x.Update(customer));

            // Act
            _mockCustomerRepository.Object.Update(customer);

            // Assert
            _mockCustomerRepository.Verify(repo => repo.Update(It.Is<Customer>(c => c.Id == customer.Id && c.Name == customer.Name)));
        }

        [Fact]
        public void UpdateCustomer_ThrowsException_WhenCustomerIsNull()
        {
            // Arrange
            Customer customer = null;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockCustomerRepository.Object.Update(customer));
        }

        [Fact]
        public void DeleteCustomer_ReturnsOkResult()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                Name = "John",
                Orders = new List<Order>
                {
                    new Order { Id = 1, OrderDate = DateTime.Now },
                    new Order { Id = 2, OrderDate = DateTime.Now }
                }
            };
            _mockCustomerRepository.Setup(x => x.Delete(customer));

            // Act
            _mockCustomerRepository.Object.Delete(customer);

            // Assert

            _mockCustomerRepository.Verify(repo => repo.Delete(It.Is<Customer>(c => c.Id == customer.Id && c.Name == customer.Name)));
        }

        [Fact]
        public void DeleteCustomer_ThrowsException_WhenCustomerIsNull()
        {
            // Arrange
            Customer customer = null;

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _mockCustomerRepository.Object.Delete(customer));
        }

    }
}
