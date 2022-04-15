using MediatR;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Events
{
    public class CreateCustomerEvent : INotification
    {
        public Guid Id { get; set; }
        public CreateCustomerEvent(Guid id)
        {
            this.Id = id;
        }

        public class CreateCustomerEmailSenderHandler : INotificationHandler<CreateCustomerEvent>
        {
            public Task Handle(CreateCustomerEvent notification, CancellationToken cancellationToken)
            {
                // IMessageSender.Send($"Welcome {notification.FirstName} {notification.LastName} !");
                return Task.CompletedTask;
            }
        }

        public class CreateCustomerLoggerHandler : INotificationHandler<CreateCustomerEvent>
        {
            private readonly ILogger<CreateCustomerLoggerHandler> _logger;

            public CreateCustomerLoggerHandler(ILogger<CreateCustomerLoggerHandler> logger)
            {
                _logger = logger;
            }

            public Task Handle(CreateCustomerEvent notification, CancellationToken cancellationToken)
            {
                _logger.LogInformation($"New customer has been created with Id: {notification.Id}");

                return Task.CompletedTask;
            }
        }
    }
}
