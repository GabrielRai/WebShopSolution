using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
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

        [Fact]
        public void GetAllCategories_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();

            // Act
            var controller = new CategoryController(fakeUoW);
            var result = controller.GetCategories();

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);

        }

        [Fact]
        public void AddCategory_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var category = new Category
            {
                Id = 1,
                Name = "Category1"
            };

            // Act
            var controller = new CategoryController(fakeUoW);
            var result = controller.AddCategory(category);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Categories.Add(A<Category>._)).MustHaveHappened();
        }

        [Fact]
        public void AddCategory_ReturnsBadRequest()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var category = new Category();
            var controller = new CategoryController(fakeUoW);
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = controller.AddCategory(null);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Categories.Add(A<Category>._)).MustNotHaveHappened();
        }

        [Fact]
        public void UpdateCategory_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var category = new Category
            {
                Id = 1,
                Name = "Category1"
            };

            // Act
            var controller = new CategoryController(fakeUoW);
            var result = controller.UpdateCategory(category);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Categories.GetById(A<int>._)).MustHaveHappened();
        }

        [Fact]
        public void UpdateCategory_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var category = new Category
            {
                Id = 1,
                Name = "Category1"
            };
            var controller = new CategoryController(fakeUoW);
            A.CallTo(() => fakeUoW.Categories.GetById(category.Id)).Returns(null);

            // Act
            var result = controller.UpdateCategory(category);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Categories.Update(A<Category>._)).MustNotHaveHappened();
        }

        [Fact]
        public void DeleteCategory_ReturnsNotFound()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var category = new Category
            {
                Id = 1,
                Name = "Category1"
            };
            var controller = new CategoryController(fakeUoW);
            A.CallTo(() => fakeUoW.Categories.GetById(category.Id)).Returns(null);

            // Act
            var result = controller.DeleteCategory(category.Id);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Categories.Delete(A<Category>._)).MustNotHaveHappened();
        }

        [Fact]
        public void DeleteCategory_ReturnsOk()
        {
            // Arrange
            var fakeUoW = A.Fake<IUnitOfWork>();
            var category = new Category
            {
                Id = 1,
                Name = "Category1"
            };
            var controller = new CategoryController(fakeUoW);
            A.CallTo(() => fakeUoW.Categories.GetById(1)).Returns(category);

            // Act
            var result = controller.DeleteCategory(category.Id);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => fakeUoW.Categories.Delete(A<Category>._)).MustHaveHappened();
        }
    }
}
