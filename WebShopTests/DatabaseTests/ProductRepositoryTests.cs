using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Repository;
using Repository.Data;
using Repository.Models;
using Repository.Repositories.Products;

namespace WebShopTests.RepositoryTests
{
    public class ProductRepositoryTests
    {
        private readonly ProductRepository _productRepository;
        private readonly MyDbContext _DbContext;
        public ProductRepositoryTests()
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
        public async Task AddProductToDatabase()
        {
            // Arrange
            var category = new Category
            {
                Name = "TestCategory"
            };

            await _DbContext.Categories.AddAsync(category);
            await _DbContext.SaveChangesAsync();

            var product = new Product
            {
                Name = "TestProduct",
                Price = 100,
                CategoryId = category.Id,
                Stock = 10
            };

            // Act
            await _DbContext.Products.AddAsync(product);
            await _DbContext.SaveChangesAsync();

            // Assert
            Assert.Equal(1, _DbContext.Products.Count());
        }
        [Fact]
        public async Task UpdateProductStock()
        {
            // Arrange
            var category = new Category
            {
                Name = "TestCategory"
            };

            await _DbContext.Categories.AddAsync(category);
            await _DbContext.SaveChangesAsync();

            var product = new Product
            {
                Name = "TestProduct",
                Price = 100,
                CategoryId = category.Id,
                Stock = 10
            };

            await _DbContext.Products.AddAsync(product);
            await _DbContext.SaveChangesAsync();

            // Act
            var result = _productRepository.UpdateProductStock(product, 20);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteProductFromDatabase()
        {
            // Arrange
           
            var category = new Category
            {
                Name = "TestCategory"
            };

            await _DbContext.Categories.AddAsync(category);
            await _DbContext.SaveChangesAsync();

            var product = new Product
            {
                Name = "TestProduct",
                Price = 100,
                CategoryId = category.Id,
                Stock = 10
            };

            await _DbContext.Products.AddAsync(product);
            await _DbContext.SaveChangesAsync();

            int totalProducts = _DbContext.Products.Count();

            // Act
            await _productRepository.Delete(product);
            await _DbContext.SaveChangesAsync();
            var totalAfterRemoval = _DbContext.Products.Count();

            // Assert
            Assert.Equal(totalProducts - 1, totalAfterRemoval);
        }
        [Fact]
        public async Task GetProductById()
        {
            // Arrange
            var category = new Category
            {
                Name = "TestCategory"
            };

            await _DbContext.Categories.AddAsync(category);
            await _DbContext.SaveChangesAsync();

            var product = new Product
            {
                Name = "TestProduct",
                Price = 100,
                CategoryId = category.Id,
                Stock = 10
            };

            await _DbContext.Products.AddAsync(product);
            await _DbContext.SaveChangesAsync();

            // Act
            var result = _productRepository.GetById(product.Id);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task GetAllProducts()
        {
            // Arrange
            var category = new Category
            {
                Name = "TestCategory"
            };

            await _DbContext.Categories.AddAsync(category);
            await _DbContext.SaveChangesAsync();

            var product = new Product
            {
                Name = "TestProduct",
                Price = 100,
                CategoryId = category.Id,
                Stock = 10
            };

            await _DbContext.Products.AddAsync(product);
            await _DbContext.SaveChangesAsync();

            // Act
            var result = _productRepository.GetAll();

            // Assert
            Assert.NotNull(result);
        }
    }
}
