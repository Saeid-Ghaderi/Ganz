using Ganz.Domain.Catalogs.Products.Events;
using MediatR;

namespace Ganz.Domain.Customers.Events
{
    public class AddNewProductNotificationHandler : INotificationHandler<AddProductSendNotificationEvent>
    {
        public Task Handle(AddProductSendNotificationEvent notification, CancellationToken cancellationToken)
        {
            //send sms to All users
            return Task.FromResult(notification);
        }
    }
}
