using Ganz.Domain.Base;
using Ganz.Domain.Catalogs.Products;

namespace Ganz.Domain.Orders
{
    public class OrderItem : Entity<Guid>
    {
        public OrderId OrderId { get; private set; }
        public ProductId ProductId { get; private set; }
        public int ItemCount { get; private set; }
        public double Price { get; set; }

        internal static OrderItem CreateNew(OrderId orderId, ProductId productId, int itemCount, double price)
        {
            return new OrderItem(orderId, productId, itemCount, price);
        }

        private OrderItem(OrderId orderId, ProductId productId, int itemCount, double price)
        {
            OrderId = orderId;
            ProductId = productId;
            ItemCount = itemCount;
            Price = price;
        }

        private OrderItem()
        {

        }
    }
}
