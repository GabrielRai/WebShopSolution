using Moq;
using Repository;
using WebShop.Notifications;

namespace WebShopTests.ObserverTests
{
    public class ObserverTests
    {
        private readonly INotificationObserver _notificationObserver;
        private readonly ProductSubject _productSubject;

        public ObserverTests()
        {
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
    }
}
