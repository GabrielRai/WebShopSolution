
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Repository;
using WebShop.Controllers;
using WebShop.UnitOfWork;

namespace WebShopTests.ControllerTests
{
    public class ProductEndpointTest
    {
        [Fact]
        public async Task GetProductById_ReturnsProduct()
        {
            // Arrange

            var FakeUoW = A.Fake<IUnitOfWork>();
            var productId = new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "test",
                Price = 10,
                Stock = 1
            };

            A.CallTo(() => FakeUoW.Products.GetById(1)).Returns(productId);

            // Act

            var controller = new ProductController(FakeUoW);
            var result = controller.GetProductById(1);

            // Assert

            var requestResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Product>(requestResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            A.CallTo(() => FakeUoW.Products.GetById(1)).Returns(null);

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.GetProductById(1);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public async Task GetProducts_ReturnsProducts()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            var products = new List<Product>
           {
               new Product { Id = 1, CategoryId = 1, Name = "test", Price = 10, Stock = 1 },
               new Product { Id = 2, CategoryId = 1, Name = "test", Price = 10, Stock = 1 }
           };

            A.CallTo(() => FakeUoW.Products.GetAll()).Returns(products);

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.GetProducts();

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Product>>(requestResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetProducts_ReturnsNotFound()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            A.CallTo(() => FakeUoW.Products.GetAll()).Returns(new List<Product>());

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.GetProducts();

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
        }

        [Fact]
        public async Task AddProduct_ReturnsOk()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            var product = new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "test",
                Price = 10,
                Stock = 1
            };

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.AddProduct(product);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => FakeUoW.Products.Add(A<Product>._)).MustHaveHappened();
        }

        [Fact]
        public async Task AddProduct_ReturnsBadRequest()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            var product = new Product();

            var controller = new ProductController(FakeUoW);
            controller.ModelState.AddModelError("Name", "Required");

            // Act
            var result = controller.AddProduct(product);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, requestResult.StatusCode);
            A.CallTo(() => FakeUoW.Products.Add(A<Product>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task UpdateProduct_ReturnsOk()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            var product = new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "test",
                Price = 10,
                Stock = 1
            };

            A.CallTo(() => FakeUoW.Products.GetById(1)).Returns(product);

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.UpdateProduct(product);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => FakeUoW.Products.Update(A<Product>._)).MustHaveHappened();
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNotFound()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            var product = new Product
            {
                Id = 1,
                CategoryId = 1,
                Name = "test",
                Price = 10,
                Stock = 1
            };

            A.CallTo(() => FakeUoW.Products.GetById(1)).Returns(null);

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.UpdateProduct(product);

            // Assert
            var requestResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => FakeUoW.Products.Update(A<Product>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task DeleteProduct_ReturnsOk()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            var product = new Product { };
            A.CallTo(() => FakeUoW.Products.GetById(1)).Returns(product);

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.DeleteProduct(product.Id);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, requestResult.StatusCode);
            A.CallTo(() => FakeUoW.Products.Delete(A<Product>._)).MustHaveHappened();
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound()
        {
            // Arrange
            var FakeUoW = A.Fake<IUnitOfWork>();
            A.CallTo(() => FakeUoW.Products.GetById(1)).Returns(null);

            // Act
            var controller = new ProductController(FakeUoW);
            var result = controller.DeleteProduct(1);

            // Assert
            var requestResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
            A.CallTo(() => FakeUoW.Products.Delete(A<Product>._)).MustNotHaveHappened();
        }
    }
}
