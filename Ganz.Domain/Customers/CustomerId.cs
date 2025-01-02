using Ganz.Domain.Base;

namespace Ganz.Domain.Customers
{
    public sealed class CustomerId:StronglyTypedId<CustomerId>
    {
        public CustomerId(Guid value):base(value)
        {
            
        }
    }
}
