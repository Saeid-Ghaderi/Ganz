using Ganz.Domain.Base;

namespace Ganz.Domain.Orders
{
    public sealed class OrderId:StronglyTypedId<OrderId>
    {
        public OrderId(Guid value):base(value)
        {
            
        }
    }
}
