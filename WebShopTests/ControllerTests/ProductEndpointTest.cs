
using FakeItEasy;
using Microsoft.AspNetCore.Http.HttpResults;
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
           var result =  controller.GetProductById(1);

           // Assert

           var requestResult = Assert.IsType<OkObjectResult>(result);
           var returnValue = Assert.IsType<Product>(requestResult.Value);
           Assert.Equal(1, returnValue.Id);
        }
    }
}
