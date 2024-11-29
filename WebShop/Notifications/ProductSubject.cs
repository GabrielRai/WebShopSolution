using Repository;

namespace WebShop.Notifications
{
    public class ProductSubject
    {
        private readonly List<INotificationObserver> _observers = new List<INotificationObserver>();
        public string State { get; set; }

        public void Attach(INotificationObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(INotificationObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(Product product)
        {
            foreach (var observer in _observers)
            {
                observer.Update(product);
            }
        }

        public void NotifyProductAdded(Product product)
        {
            State = ("Product added: " + product.Name);
            Notify(product);

        }

        public List<INotificationObserver> GetObservers()
        {
            return _observers;
        }
    }
}
