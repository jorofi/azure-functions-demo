using FunctionsDemo.Abstractions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FunctionsDemo.Functions;

public class OrdersTrackerFunction
{
    private readonly ILogger _logger;

    public OrdersTrackerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<OrdersTrackerFunction>();
    }

    [Function(nameof(OrdersTrackerFunction))]
    [ServiceBusOutput(Constants.EmailQueue, Connection = Constants.ServiceBusConnectionName)]
    public EmailMessages Run([CosmosDBTrigger(
        databaseName: Constants.CosmosDBDatabaseName,
        containerName: Constants.CosmosDBContainerName,
        Connection = Constants.CosmosDBConnectionName,
        LeaseContainerName = Constants.CosmosDBLeaseContainerName,
        CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Order> ordersInput)
    {
        _logger.LogInformation("Orders modified: " + ordersInput.Count);

        List<EmailMessage> messages = new List<EmailMessage>();

        foreach (var order in ordersInput)
        {
            _logger.LogInformation("Processing order {id}", order.Id);

            messages.Add(new EmailMessage()
            {
                To = order.Email,
                Subject = $"Your order {order.Id} has been processed.",
                Body = $"Your {order.ProductName} has been activated. For the amount of {order.Price}"
            });
        }

        return new EmailMessages()
        {
            Messages = messages.ToArray()
        };
    }
}
