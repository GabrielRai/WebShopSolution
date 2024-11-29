using Repository.Classes;
using Repository.Models;
using WebShop.UnitOfWork;

namespace Repository.Services
{
    public class OrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateOrderAsync(CreateOrderRequest request)
        {
            if (_unitOfWork == null || _unitOfWork.Customers == null)
            {
                return false;
            }

            if (request == null || request.CustomerId == 0)
            {
                return false;
            }

            var customer = _unitOfWork.Customers.GetById(request.CustomerId);

            if (customer == null)
            {
                return false; // Returnera false istället för att kasta exception
            }

            var order = new Order
            {
                Customer = customer,
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                OrderDate = request.OrderDate,
                OrderItems = new List<OrderItem>()
            };

            foreach (var productRequest in request.Products)
            {
                var product = _unitOfWork.Products.GetById(productRequest.ProductId);
                if (product == null || product.Stock < productRequest.Quantity)
                {
                    return false; // Felaktig produkt eller otillräckligt lager
                }

                var orderItem = new OrderItem
                {
                    Order = order,
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = productRequest.Quantity,
                    Product = product
                };

                order.OrderItems.Add(orderItem);
                product.Stock -= productRequest.Quantity;
                _unitOfWork.Products.Update(product);
            }

            var result = _unitOfWork.Orders.CreateOrder(order);

            if (result)
            {
                await _unitOfWork.Complete();
                return true;
            }
            return false;
        }
    }
}
