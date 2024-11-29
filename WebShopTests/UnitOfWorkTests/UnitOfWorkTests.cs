using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Repository;
using Repository.Data;
using Repository.Repositories.Products;
using WebShop.Notifications;
using WebShop.UnitOfWork;

namespace WebShopTests.UnitOfWorkTests
{
    public class UnitOfWorkTests
    {
        private readonly ProductRepository _productRepository;
        private readonly MyDbContext _DbContext;
        private readonly INotificationObserver _notificationObserver;
        private readonly ProductSubject _productSubject;

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
            _notificationObserver = new EmailNotification();
            _productSubject = new ProductSubject();
        }
        [Fact]
        public void Attach_AddsObserverToSubject()
        {
            // Arrange
            var productSubject = new ProductSubject();
            var customer = new Mock<INotificationObserver>();

            // Act
            productSubject.Attach(customer.Object);

            // Assert
            Assert.Contains(customer.Object, productSubject.GetObservers());
        }
        [Fact]
        public void Detach_RemovesObserverFromSubject()
        {
            // Arrange
            var productSubject = new ProductSubject();
            var customer = new Mock<INotificationObserver>();
            productSubject.Attach(customer.Object);

            // Act
            productSubject.Detach(customer.Object);

            // Assert
            Assert.DoesNotContain(customer.Object, productSubject.GetObservers());
        }
        [Fact]
        public void Notify_CallsObserverUpdate()
        {
            // Arrange
            var customer = new Mock<INotificationObserver>();
            var product = new Product
            {
                Name = "UoWTestProduct",
                Price = 100,
                CategoryId = 1,
                Stock = 10
            };

            customer.Setup(x => x.Update(product));
            _productSubject.Attach(customer.Object);

            // Act

            _productSubject.Notify(product);

            // Assert

            customer.Verify(x => x.Update(product), Times.Once);

        }
        [Fact]
        public void NotifyProductAdd_CallsObserverUpdate()
        {
            // Arrange
            var customer = new Mock<INotificationObserver>();
            var product = new Product
            {
                Name = "UoWTestProduct",
                Price = 100,
                CategoryId = 1,
                Stock = 10
            };

            customer.Setup(x => x.Update(It.Is<Product>(p => p.Stock == 15)));
            _productSubject.Attach(customer.Object);

            // Act

            _productSubject.NotifyProductAdded(product);

            // Assert

            customer.Verify(x => x.Update(product), Times.Once);

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
