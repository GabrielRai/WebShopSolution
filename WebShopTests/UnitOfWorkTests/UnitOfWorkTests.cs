using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repository;
using Repository.Data;
using Repository.Repositories.Products;
using WebShop.UnitOfWork;

namespace WebShopTests.UnitOfWorkTests
{
    public class UnitOfWorkTests
    {
        private readonly ProductRepository _productRepository;
        private readonly MyDbContext _DbContext;

        public UnitOfWorkTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            _DbContext = new MyDbContext(options);
            _productRepository = new ProductRepository(_DbContext, _DbContext.Products);
        }
        [Fact]
        public void NotifyProductAdded_CallsObserverUpdate()
        {
        }
        [Fact]
        public async Task AddProduct_SaveToDatabase()
        {
            // Arrange
            var _unitOfWork = new UnitOfWork(_DbContext);
            var product = new Product
            {
                Name = "UoWTestProduct",
                Price = 100,
                CategoryId = 1,
                Stock = 10
            };

            // Act

            await _unitOfWork.Products.Add(product);
            await _unitOfWork.Complete();

            // Assert

            var savedProduct = await _DbContext.Products.FindAsync(product.Id);
            Assert.NotNull(savedProduct);
            Assert.Equal(product, savedProduct);

        }
        [Fact]
        public async Task UpdateProduct_SaveToDatabase()
        {
            // Arrange
            var _unitOfWork = new UnitOfWork(_DbContext);
            var product = new Product
            {
                Name = "UoWTestProduct",
                Price = 100,
                CategoryId = 1,
                Stock = 10
            };

            await _DbContext.Products.AddAsync(product);
            await _DbContext.SaveChangesAsync();

            // Act

            product.Stock = 20;
            await _unitOfWork.Products.Update(product);
            await _unitOfWork.Complete();

            // Assert

            var updatedProduct = await _DbContext.Products.FindAsync(product.Id);
            Assert.Equal(20, updatedProduct.Stock);

        }
        [Fact]
        public async Task DeleteProduct_SaveToDatabase()
        {
            // Arrange
            var unitOfWork = new UnitOfWork(_DbContext);
            var product = new Product
            {
                Name = "UoWTestProduct",
                Price = 100,
                CategoryId = 1,
                Stock = 10
            };

            await _DbContext.Products.AddAsync(product);
            await _DbContext.SaveChangesAsync();

            // Act

            await unitOfWork.Products.Delete(product);
            await unitOfWork.Complete();

            // Assert

            var deletedProduct = await _DbContext.Products.FindAsync(product.Id);
            Assert.Null(deletedProduct);

        }
    }
}
