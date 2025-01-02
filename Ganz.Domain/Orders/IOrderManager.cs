using Ganz.Domain.Customers;
using Ganz.Domain.Shared;

namespace Ganz.Domain.Orders
{
    public interface IOrderManager
    {
        Task RegisterOrder(CustomerId customerId, string address, string postalCode, string phone, List<OrderItemData> orderItems);
        Task CancelOrder(OrderId orderId, CustomerId customerId);
        Task CancelOrderWithAdmin(OrderId orderId, CustomerId customerId);
    }
}
