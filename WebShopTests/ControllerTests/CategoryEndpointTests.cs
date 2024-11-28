using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;
using WebShop.Controllers;
using WebShop.UnitOfWork;

namespace WebShopTests.ControllerTests
{
    public class CategoryEndpointTests
    {
        [Fact]
        public void GetAllCategories_ReturnsOkResult_WithAListOfCategories()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1, Name = "Category1",
                },
                new Category
                {
                    Id = 2, 
                    Name = "Category2",
                }
            };
            A.CallTo(() => fakeUoW.Categories.GetAll()).Returns(categories);

            // Act
            var controller = new CategoryController(fakeUoW);
            var result = controller.GetCategories();

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Category>>(requestResult.Value);
            Assert.Equal(categories, returnValue);
        }
    }
}
