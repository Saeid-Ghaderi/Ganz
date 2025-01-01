using Ganz.Domain.Base;

namespace Ganz.Domain.Catalogs.Products.Events
{
    public record class AddProductSendNotificationEvent : DomainEvent
    {
        public ProductId ProductId { get; init; }

        public AddProductSendNotificationEvent(ProductId productId)
        {
            ProductId = productId;
            AggregateId = productId.Value;
        }
    }
}
