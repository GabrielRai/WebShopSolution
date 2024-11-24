using Microsoft.AspNetCore.Mvc;
using Moq;
using Repository;
using Repository.Repositories.Products;

public class ProductRepositoryTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;

    public ProductRepositoryTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
    }

    [Fact]
    public void GetProducts_ReturnsOkResult_WithAListOfProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1" },
            new Product { Id = 2, Name = "Product 2" }
        };

        _mockProductRepository.Setup(repo => repo.GetAll()).Returns(products);

        // Act
        var result = _mockProductRepository.Object.GetAll();

        // Assert
        Assert.Equal(products, result);
    }

    [Fact]
    public void GetProducts_ReturnsNotFoundResult()
    {
        // Arrange
        _mockProductRepository.Setup(repo => repo.GetAll()).Returns(() => null);

        // Act
        var result = _mockProductRepository.Object.GetAll();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetProductById_ReturnsOkResult_WithAProduct()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product 1" };
        _mockProductRepository.Setup(repo => repo.GetById(product.Id)).Returns(product);

        // Act
        var result = _mockProductRepository.Object.GetById(product.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(product, result);
    }

    [Fact]
    public void GetProductById_ReturnsNotFoundResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product 1" };
        _mockProductRepository.Setup(repo => repo.GetById(product.Id)).Returns(() => null);

        // Act
        var result = _mockProductRepository.Object.GetById(product.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void AddProduct_ReturnsOkResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product 1" };
        _mockProductRepository.Setup(repo => repo.Add(product));

        // Act
        _mockProductRepository.Object.Add(product);
        
        // Assert
        _mockProductRepository.Verify(repo => repo.Add(It.Is<Product>(p => p.Id == product.Id && p.Name == product.Name)), Times.Once);
    }

    [Fact]
    public void AddProduct_ThrowsException_WhenProductIsNull()
    {
        // Arrange
        Product product = null;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _mockProductRepository.Object.Add(product));
    }

    [Fact]
    public void UpdateProduct_ReturnsOkResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Test updating Product 1" };
        _mockProductRepository.Setup(repo => repo.Update(product));

        // Act
       _mockProductRepository.Object.Update(product);

        // Assert
        _mockProductRepository.Verify(repo => repo.Update(It.Is<Product>(p => p.Id == product.Id && p.Name == product.Name)), Times.Once);
    }

    [Fact]
    public void UpdateProduct_ThrowsException_WhenProductIsNull()
    {
        // Arrange
        Product product = null;

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _mockProductRepository.Object.Update(product));
    }

    [Fact]
    public void DeleteProduct_ReturnsOkResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product 1" };
        _mockProductRepository.Setup(repo => repo.Delete(product));

        // Act
        _mockProductRepository.Object.Delete(product);
        var result = _mockProductRepository.Object.GetById(product.Id);

        // Assert
        Assert.Null(result);
    }
    [Fact]
    public void DeleteProduct_ReturnsNotFoundResult()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product 1" };
        _mockProductRepository.Setup(repo => repo.Delete(product));

        // Act
        _mockProductRepository.Object.Delete(product);

        // Assert
        _mockProductRepository.Verify(repo => repo.Delete(It.Is<Product>(p => p.Id == product.Id && p.Name == product.Name)), Times.Once);
    }

    [Fact]
    public void UpdateStock_ReturnsTrueIfDone()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product 1", Stock = 10 };
        var quantity = 5;

        _mockProductRepository.Setup(repo => repo.UpdateProductStock(product, quantity)).Returns(true);

        // Act

        var result = _mockProductRepository.Object.UpdateProductStock(product, quantity);

        // Assert

        Assert.True(result);
        _mockProductRepository.Verify(repo => repo.UpdateProductStock(It.Is<Product>(p => p.Id == product.Id && p.Stock == product.Stock), quantity), Times.Once);
    }

    [Fact]
    public void UpdateStock_ReturnsFalseIfNotDone()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product 1", Stock = 10 };
        var quantity = 5;

        _mockProductRepository.Setup(repo => repo.UpdateProductStock(product, quantity)).Returns(false);

        // Act

        var result = _mockProductRepository.Object.UpdateProductStock(product, quantity);

        // Assert

        Assert.False(result);
        _mockProductRepository.Verify(repo => repo.UpdateProductStock(It.Is<Product>(p => p.Id == product.Id && p.Stock == product.Stock), quantity), Times.Once);
    }
}
