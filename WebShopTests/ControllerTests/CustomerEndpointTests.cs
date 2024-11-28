using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using WebShop.Controllers;
using WebShop.UnitOfWork;

namespace WebShopTests.ControllerTests
{
    public class CustomerEndpointTests
    {
        [Fact]
        public void GetAllCustomers_ReturnsOkResult_WithAListOfCustomers()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
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
            A.CallTo(() => fakeUoW.Customers.GetAll()).Returns(customers);

            // Act
            var controller = new CustomerController(fakeUoW);
            var result = controller.GetCustomers();

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Customer>>(requestResult.Value);
            Assert.Equal(customers, returnValue);
        }

        [Fact]
        public void GetAllCustomers_ReturnsNull()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            A.CallTo(() => fakeUoW.Customers.GetAll()).Returns(new List<Customer>());

            // Act
            var controller = new CustomerController(fakeUoW);
            var result = controller.GetCustomers();

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public void AddCustomer_ReturnOk() {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
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

            var controller = new CustomerController(fakeUoW);

            // Act
            var result = controller.AddCustomer(customer);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Customers.Add(A<Customer>._)).MustHaveHappened();
        }

        [Fact]
        public void AddCustomer_ReturnsBadRequest_WhenCustomerIsInvalid()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var customer = new Customer();

            var controller = new CustomerController(fakeUoW);
            controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = controller.AddCustomer(customer);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Customers.Add(A<Customer>._)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateCustomer_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
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

            A.CallTo(() => fakeUoW.Customers.GetById(1)).Returns(customer);

            var controller = new CustomerController(fakeUoW);

            // Act
            var result = controller.UpdateCustomer(customer);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Customers.Update(A<Customer>._)).MustHaveHappened();
        }

        [Fact]
        public void UpdateCustomer_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
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

            A.CallTo(() => fakeUoW.Customers.GetById(1)).Returns(null);

            var controller = new CustomerController(fakeUoW);

            // Act
            var result = controller.UpdateCustomer(customer);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Customers.Update(A<Customer>._)).MustNotHaveHappened();
        }

        [Fact]
        public void DeleteCustomer_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
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

            A.CallTo(() => fakeUoW.Customers.GetById(1)).Returns(customer);

            var controller = new CustomerController(fakeUoW);

            // Act
            var result = controller.DeleteCustomer(1);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Customers.Delete(A<Customer>._)).MustHaveHappened();
        }

        [Fact]
        public void DeleteCustomer_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
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

            A.CallTo(() => fakeUoW.Customers.GetById(1)).Returns(null);

            var controller = new CustomerController(fakeUoW);

            // Act
            var result = controller.DeleteCustomer(1);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Customers.Delete(A<Customer>._)).MustNotHaveHappened();
        }
    }
}
