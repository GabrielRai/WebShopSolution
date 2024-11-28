using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Repository.Repositories.OrderItems;

namespace WebShopTests.UnitTests
{
    public class OrderItemUnitTest
    {
        private readonly Mock<IOrderItemRepository> _mockOrderRepository;
        public OrderItemUnitTest()
        {
            _mockOrderRepository = new Mock<IOrderItemRepository>();
        }

    }
}
